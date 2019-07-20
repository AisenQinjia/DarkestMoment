using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//巡逻型敌人
public class PatrolEnemyController : BaseRoleController
{
    //协程
    IEnumerator coroutine;

    GameObject redAlarm;
    GameObject yellowAlarm;
    GameObject viewSprite;
    Animator animator;
    //预警计时
    float alarmTimer = 0;
    //正面预警时间
    public float frontAlarmTime;
    //背面预警时间
    public float backKillTime;
    //发现感叹号持续时间
    public float redAlarmLastTime;

    public GameObject player;
    public float walkSpeed;
    public float chaseSpeed;
    public float chaseRange;
    public float attackRange;
    public float retreatSpeed;
    public float retreatInitialSpeed;
    public float retreatInitialSpeedLastTime;
    public float chaseAngle;
    public float stareTime;
    public float stareRange;

    //暂时都用一个collider吧，用距离判断生效与否
    public float barrierValidDistance;
    public Transform[] path;

    // Start is called before the first frame update
    void Start()
    {
        player = RoleManager.Instance.GetPlayer().gameObject;
        GetResource();
        CreateFSM();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        Statemanager.currentState.ReState(player, gameObject);
        Statemanager.currentState.Update(player, gameObject);
    }

    void CreateFSM()
    {
        Statemanager = new EnemyStateManager();

        WalkStateForPatrol walk = new WalkStateForPatrol(path, walkSpeed);
        walk.AddTransition(Transition.ShouldTurn, StateID.Turn);
        walk.AddTransition(Transition.SawPlayer, StateID.Chase);
        walk.AddTransition(Transition.FeelSomethingWrong, StateID.Stop);

        TurnState turn = new TurnState();
        turn.AddTransition(Transition.ShouldWalk, StateID.Walk);

        ChaseState chase = new ChaseState(chaseSpeed, chaseRange, attackRange, chaseAngle);
        chase.AddTransition(Transition.CanAttack, StateID.Attack);
        chase.AddTransition(Transition.LostPlayer, StateID.Retreat);
        chase.AddTransition(Transition.TouchedBarrier, StateID.StareAtPlayer);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.LostPlayer, StateID.Retreat);

        RetreatState retreat = new RetreatState(path, retreatSpeed, retreatInitialSpeed, retreatInitialSpeedLastTime);
        retreat.AddTransition(Transition.ShouldWalk, StateID.Walk);
        retreat.AddTransition(Transition.SawPlayer, StateID.Chase);

        StareAtPlayerState stareAtPlayer = new StareAtPlayerState(stareTime, stareRange, chaseAngle);
        stareAtPlayer.AddTransition(Transition.LostPlayer, StateID.Retreat);
        stareAtPlayer.AddTransition(Transition.CanAttack, StateID.Attack);
        stareAtPlayer.AddTransition(Transition.CanChase, StateID.Chase);

        StopState stop = new StopState();
        stop.AddTransition(Transition.LostPlayer, StateID.Walk);
        stop.AddTransition(Transition.SawPlayer, StateID.Chase);

        Statemanager.AddState(walk);
        Statemanager.AddState(turn);
        Statemanager.AddState(chase);
        Statemanager.AddState(attack);
        Statemanager.AddState(retreat);
        Statemanager.AddState(stareAtPlayer);
        Statemanager.AddState(stop);
    }

    void GetResource()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        yellowAlarm = gameObject.transform.Find("YellowAlarm").gameObject;
        redAlarm = gameObject.transform.Find("RedAlarm").gameObject;
        if (yellowAlarm == null || redAlarm == null) Debug.LogError("no alarm!");
        viewSprite = gameObject.transform.Find("ViewSprite").gameObject;
        if (viewSprite == null) Debug.LogError("no ViewSprite!");
        animator = gameObject.GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("no animator!");

    }
    public void PerformTransition(Transition trans, GameObject player, GameObject enemy)
    {
        Statemanager.PerformTransition(trans, player, enemy);
    }

    void UpdateTimer()
    {
        if (alarmTimer != 0)
        {
            alarmTimer += Time.deltaTime;
        }
    }
    //进入视野
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Statemanager.currentStateID == StateID.Walk)
        {
            PerformTransition(Transition.FeelSomethingWrong, player, gameObject);
            yellowAlarm.SetActive(true);
            redAlarm.SetActive(false);
            //相当于触发器，让timer开始计时
            alarmTimer = 0.01f;
        }
        else if (other.gameObject.tag == "Player")
        {
            //背后发现需要调转方向
            if (!IsInForntOfEnemy(player, gameObject))
            {
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
            }
            PerformTransition(Transition.SawPlayer, player, gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //这令人窒息的判断...虽然目前只有chase可以转换到Stare态
        if (other.gameObject.tag == "Barrier" && Statemanager.currentStateID == StateID.Chase && (transform.position - other.gameObject.transform.position).magnitude < barrierValidDistance)
        {
            Statemanager.PerformTransition(Transition.TouchedBarrier, player, gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            if (alarmTimer > frontAlarmTime)
            {
                if (IsInForntOfEnemy(player, gameObject))
                {
                    RedAlarmBegin();
                    LeaveWalkState();
                    Statemanager.PerformTransition(Transition.SawPlayer, player, gameObject);
                }
                else if (alarmTimer > backKillTime)
                {
                    RedAlarmBegin();
                    LeaveWalkState();
                    LookAtPlayer(player, gameObject);
                    Statemanager.PerformTransition(Transition.SawPlayer, player, gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Statemanager.currentStateID == StateID.Stop)
        {
            PerformTransition(Transition.LostPlayer, player, gameObject);
            yellowAlarm.SetActive(false);
            redAlarm.SetActive(false);
            alarmTimer = 0;
        }
    }

    void RedAlarmBegin()
    {
        alarmTimer = 0;
        yellowAlarm.SetActive(false);
        redAlarm.SetActive(true);
        coroutine = WaitAndDeactive(redAlarmLastTime);
        StartCoroutine(coroutine);
    }

    //回到Walk状态需要做的事
    public override void ReturnToWalkState()
    {
        if (viewSprite != null) viewSprite.SetActive(true);
    }

    //离开Walk状态需要做的事
    public override void LeaveWalkState()
    {
        if (viewSprite != null) viewSprite.SetActive(false);
    }

    //攻击
    public override void Attack()
    {
        animator.SetTrigger("canAttack");
        player.GetComponent<BaseRoleController>().OnDead();
    }

    public override void WaitTime(float time)
    {

    }
    //等待
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    void EnemyStop()
    {

    }

    //死亡接口
    public override void OnDead()
    {
        //Debug.LogError(" I am Dead!");
        Destroy(this.gameObject);
    }

    //吸引敌人注意力接口
    public override void ComeToMe(Transform trans)
    {
        //if()
        Statemanager.PerformTransition(Transition.HeardNoise, player, gameObject);
    }

    //红色感叹号延时消失
    IEnumerator WaitAndDeactive(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        redAlarm.SetActive(false);
    }
}
