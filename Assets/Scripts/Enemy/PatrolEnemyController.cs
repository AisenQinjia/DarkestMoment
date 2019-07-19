using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//巡逻型敌人
public class PatrolEnemyController : BaseRoleController
{
    public GameObject player;
    public float walkSpeed;
    public float chaseSpeed;
    public float chaseRange;
    public float attackRange;
    public float retreatSpeed;
    public float viewAngle;
    public float stareTime;
    public float stareRange;
    //暂时都用一个collider吧，用距离判断生效与否
    public float barrierValidDistance;
    public Transform[] path;

    // Start is called before the first frame update
    void Start()
    {
        CreateFSM();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Statemanager.currentState.ReState(player, gameObject);
        Statemanager.currentState.Update(player, gameObject);
    }

    void CreateFSM()
    {
        Statemanager = new EnemyStateManager();
        WalkStateForPatrol walk = new WalkStateForPatrol(path, walkSpeed);
        walk.AddTransition(Transition.ShouldTurn, StateID.Turn);
        walk.AddTransition(Transition.SawPlayer, StateID.Chase);

        TurnState turn = new TurnState();
        turn.AddTransition(Transition.ShouldWalk, StateID.Walk);

        ChaseState chase = new ChaseState(chaseSpeed, chaseRange, attackRange);
        chase.AddTransition(Transition.CanAttack, StateID.Attack);
        chase.AddTransition(Transition.LostPlayer, StateID.Retreat);
        chase.AddTransition(Transition.TouchedBarrier, StateID.StareAtPlayer);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.LostPlayer, StateID.Retreat);

        RetreatState retreat = new RetreatState(path, retreatSpeed);
        retreat.AddTransition(Transition.ShouldWalk, StateID.Walk);
        retreat.AddTransition(Transition.SawPlayer, StateID.Chase);

        StareAtPlayerState stareAtPlayer = new StareAtPlayerState(stareTime, stareRange, viewAngle);
        stareAtPlayer.AddTransition(Transition.LostPlayer, StateID.Retreat);
        stareAtPlayer.AddTransition(Transition.CanAttack, StateID.Attack);
        stareAtPlayer.AddTransition(Transition.CanChase, StateID.Chase);


        Statemanager.AddState(walk);
        Statemanager.AddState(turn);
        Statemanager.AddState(chase);
        Statemanager.AddState(attack);
        Statemanager.AddState(retreat);
        Statemanager.AddState(stareAtPlayer);
    }

    public void PerformTransition(Transition trans)
    {
        Statemanager.PerformTransition(trans);
    }

    //进入视野
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Statemanager.PerformTransition(Transition.SawPlayer);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //这令人绝望的判断...
        if (other.gameObject.tag == "Barrier" && Statemanager.currentStateID == StateID.Chase && (transform.position - other.gameObject.transform.position).magnitude < barrierValidDistance)
        {
            Statemanager.PerformTransition(Transition.TouchedBarrier);
        }
    }


    //死亡接口
    public override void OnDead()
    {
        Debug.LogError(" I am Dead!");
    }

    //吸引敌人注意力接口
    public override void ComeToMe(Transform trans)
    {
        Statemanager.PerformTransition(Transition.HeardNoise);
    }
}
