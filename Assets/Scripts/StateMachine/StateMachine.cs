//状态机相关代码
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人状态
public enum StateID
{
    NullState,
    Walk,
    Turn,
    Chase,
    Attack,
    Retreat,
    CheckPoint,
    StareAtPlayer,
    Stop
}

//过渡
public enum Transition
{
    NullTransition,
    ShouldTurn,
    ShouldWalk,
    ShouldStop,
    SawPlayer,
    CanChase,
    CanAttack,
    LostPlayer,
    HeardNoise,
    TouchedBarrier,
    FeelSomethingWrong
}

public class EnemyStateManager
{
    private List<FSMState> states;
    private StateID realCurrentStateID;
    public StateID currentStateID {
        set { realCurrentStateID = value; }
        get { return realCurrentStateID; }
    }
    private FSMState realCurrentState;
    public FSMState currentState {
        set { realCurrentState = value; }
        get { return realCurrentState; }
    }

    public EnemyStateManager()
    {
        states = new List<FSMState>();
    }

    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("添加的状态机不存在");
            return;
        }
        if(states.Count == 0)
        {
            states.Add(state);
            currentState = state;
            currentStateID = state.stateID;
            return;
        }
        foreach (FSMState st in states)
        {
            if (st.stateID == state.stateID)
            {
                Debug.LogError("FSM ERROR: 无法添加状态 " + state.stateID.ToString() + ", 因为该状态已存在");
                return;
            }
        }
        states.Add(state);
    }

    public FSMState GetState(StateID stateid)
    {
        foreach (FSMState st in states)
        {
            if (st.stateID == stateid)
            {
                return st;
            }
        }
        Debug.LogError("manager 不存在: " + stateid.ToString() + " 状态");
        return states[0];
    }

    public void DeleteState(StateID id) { }

    public void PerformTransition(Transition trans, GameObject player, GameObject enemy)
    {
        if(trans == Transition.NullTransition)
        {
            Debug.LogError("trans is null!");
            return;
        }
        
        StateID id = currentState.GetState(trans);
        if (id == StateID.NullState)
        {
            Debug.Log("当前状态: " + currentState.stateID.ToString() + " 没有 "+ trans.ToString() + " 行为");
            return;
        }

        currentStateID = id;
        bool found = false;
        foreach(FSMState st in states)
        {
            if (st.stateID == currentStateID)
            {
                found = true;
                currentState.DoBeforeLeaving(player, enemy);
                currentState = st;
                currentState.DoBeforeEntering(player, enemy);
                break;
            }
        }
        if (!found)
        {
            Debug.Log("状态机没有添加" + currentStateID.ToString() + "对应状态");
        }
    }
}
//抽象基类
public abstract class FSMState
{
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    public StateID stateID;

    public virtual void DoBeforeEntering(GameObject player, GameObject enemy) { }

    public virtual void DoBeforeLeaving(GameObject player, GameObject enemy) { }
 
    public virtual void ReState(GameObject player, GameObject enemy) { }

    public virtual void Update(GameObject player, GameObject enemy) { }

    public void AddTransition(Transition trans, StateID id)
    {
        if(trans == Transition.NullTransition || id == StateID.NullState)
        {
            Debug.LogError("null add!");
            return;
        }
        if(map.ContainsKey(trans))
        {
            Debug.LogErrorFormat("Already has the transition: {0}", trans.ToString());
            return;
        }
        map.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
    }

    public StateID GetState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        return StateID.NullState;
    }

    //看着行进方向
    protected void LookAtDirection(GameObject player, GameObject enemy, Vector3 moveDir)
    {
        moveDir = new Vector3(moveDir.x, 0, 0);
        if (Mathf.Abs(Vector3.Angle(moveDir, enemy.transform.right)) > 90)
        {
            enemy.transform.Rotate(new Vector3(0, 180, 0));
            ReverseSprite(player, enemy);
        }
    }

    //背对着行进方向
    protected void BackAtDirection(GameObject player, GameObject enemy, Vector3 moveDir)
    {
        moveDir = new Vector3(moveDir.x, 0, 0);
        if (Mathf.Abs(Vector3.Angle(moveDir, enemy.transform.right)) < 90)
        {
            enemy.transform.Rotate(new Vector3(0, 180, 0));
            ReverseSprite(player, enemy);
        }
    }

    //执行过渡
    public void PerformTransition(Transition trans, GameObject player, GameObject enemy)
    {
        enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(trans, player, enemy);
    }

    //计算是否在目标视野范围
    protected bool IsInRange(GameObject player, GameObject enemy, float angle, float range)
    {
        Vector3 dir = player.transform.position - enemy.transform.position;
        return Mathf.Abs(Vector3.Angle(enemy.transform.right, dir)) < angle && dir.magnitude < range;
    }

    //计算距离
    protected float Distance(GameObject player, GameObject enemy)
    {
        return (player.transform.position - enemy.transform.position).magnitude;
    }

    //判断player是否在enemy左侧
    protected bool IsEnemyLeft(GameObject player, GameObject enemy)
    {
        Vector3 dir = player.transform.position - enemy.transform.position;
        return dir.x < 0;
    }

    protected void ReverseSprite(GameObject player, GameObject enemy)
    {
        GameObject go = enemy.transform.Find("Slime_0").gameObject;
        go.transform.Rotate(new Vector3(0, 180, 0));
        bool isFlipX = go.GetComponent<SpriteRenderer>().flipX;
        go.GetComponent<SpriteRenderer>().flipX = !isFlipX;
    }
}

//巡逻敌人行走状态
public  class WalkStateForPatrol : FSMState
{
    private float speed;
    private Transform[] pathPoints;
    private int currentPointIndex;
    private float endPoint;

    public WalkStateForPatrol(Transform[] path, float vel)
    {
        speed = vel;
        pathPoints = path;
        stateID = StateID.Walk;
    }
    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("Enter Walk");
        enemy.gameObject.GetComponent<BaseRoleController>().ReturnToWalkState();
    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave Walk");

        enemy.gameObject.GetComponent<BaseRoleController>().LeaveWalkState();
    }

    public override void ReState(GameObject player, GameObject enemy)
    {

    }

    public override void Update(GameObject player, GameObject enemy)
    {
        Vector3 moveDir = pathPoints[currentPointIndex].position - enemy.transform.position;
        if (moveDir.magnitude < 2.6)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Length)
            {
                currentPointIndex = 0;
            }
           // Debug.Log("currentPointIndex: " + pathPoints[currentPointIndex].position.x);
            enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.ShouldTurn,  player,  enemy);
        }
        else
        {
            moveDir = new Vector3(moveDir.x, 0, 0);
            LookAtDirection(player, enemy, moveDir);
            Vector3 vel = moveDir.normalized * speed;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, 0);
        }
    }
}


//只能转向敌人行走状态
public class WalkStateForTurn : FSMState
{

}

//触发器敌人行走状态
public class WalkStateForTrigger : FSMState
{

}

//转向
public class TurnState : FSMState
{
    public TurnState()
    {
        stateID = StateID.Turn;
    }
    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("enter turn");

    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave turn");

    }
    public override void ReState(GameObject player, GameObject enemy) { }

    public override void Update(GameObject player, GameObject enemy)
    {
        enemy.transform.Rotate(new Vector3(0, 180, 0));
        ReverseSprite(player, enemy);
        enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.ShouldWalk,  player,  enemy);
    }
}

//追击
public class ChaseState : FSMState
{
    private float speed;
    private float chaseRange;
    private float chaseAngle;
    private float attackRange;

    public ChaseState(float vel, float chaserange, float attackrange, float chaseangle)
    {
        stateID = StateID.Chase;
        speed = vel;
        chaseRange = chaserange;
        attackRange = attackrange;
        chaseAngle = chaseangle;
    }

    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("enter chase");

    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave chase");

    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        if(!IsInRange(player, enemy, chaseAngle, chaseRange))
        {
            Debug.Log("lost player");
            PerformTransition(Transition.LostPlayer,  player,  enemy);
        }
        else if(Distance(player, enemy) < attackRange)
        {
            Debug.Log("in attackRange");
            PerformTransition(Transition.CanAttack,  player,  enemy);
        }
    }

    public override void Update(GameObject player, GameObject enemy)
    {
        Vector3 moveDir = player.transform.position - enemy.transform.position;
        LookAtDirection(player, enemy, moveDir);
        Vector3 vel = moveDir.normalized * speed;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, 0);
    }
}

//攻击
public class AttackState : FSMState
{
    Animator animator;
    AnimatorStateInfo animInfo;
    float timer;
    public AttackState(Animator anim)
    {
        animator = anim;
        stateID = StateID.Attack;
        timer = 0;
    }

    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("enter attack");
        enemy.GetComponent<BaseRoleController>().Attack();
        if (animator == null) Debug.Log("no animator");
        enemy.GetComponent<BaseRoleController>().rigidbody.velocity = new Vector2(0, 0);
    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave Attack");
    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log("time: " + stateinfo.normalizedTime);stateinfo.normalizedTime > 1.7f ||
        if ( timer > 1.0f)
        {
            Debug.Log("timer > 1");
            enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.LostPlayer, player, enemy);
        }
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        //Debug.LogError("Die !");
        timer += Time.deltaTime;
    }
}

//后退
public class RetreatState : FSMState
{
    //就是巡逻的两个点
    Transform[] paths;
    //正常回退速度
    float speed;
    //开始的回退速度
    float initialSpeed;
    //开始回退速度持续时间
    float initialSpeedLastTime;
    float timer;

    float minX;
    float maxX;

    public RetreatState(Transform[] path, float vel, float initialSp, float initialSpTime)
    {
        timer = 0;
        initialSpeed = initialSp;
        initialSpeedLastTime = initialSpTime;
        stateID = StateID.Retreat;
        paths = path;
        speed = vel;
        GetXRange();
    }

    public void GetXRange()
    {
        //就先假设只有两个点吧
        minX = paths[0].position.x;
        maxX = paths[1].position.x;

        if(minX > maxX)
        {
            float tmp = minX;
            minX = maxX;
            maxX = tmp;
        }
    }
    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("enter retreate");
        timer = 0;
    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("leave retreate");
        timer = 0;
    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        if(enemy.transform.position.x > minX && enemy.transform.position.x < maxX)
        {
            PerformTransition(Transition.ShouldWalk,  player,  enemy);
        }
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        timer += Time.deltaTime;
        if(enemy.transform.position.x < minX)
        {
            if(timer < initialSpeedLastTime)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(initialSpeed, 0);
            }
            else enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else if(enemy.transform.position.x > maxX)
        {
            if (timer < initialSpeedLastTime)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-initialSpeed, 0);
            }
            else enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        

    }
}

//目视敌人状态
public class StareAtPlayerState : FSMState
{
    float validTime;
    float validRange;
    float validAngle;
    float timer;

    public StareAtPlayerState(float vlidtime, float starerange, float validangle)
    {
        stateID = StateID.StareAtPlayer;
        validTime = vlidtime;
        validRange = starerange;
        validAngle = validangle;
    }

    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("Enter stare");
        timer = 0;
    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave stare");

        timer = 0;
    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        if (IsInRange(player, enemy, validAngle, validRange))
        {
            timer = 0;
        }
        else
        {
            if (timer > validTime)
            {
                PerformTransition(Transition.LostPlayer, player,  enemy);
            }
        }
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        timer = timer + Time.deltaTime;
        enemy.GetComponent<BaseRoleController>().rigidbody.velocity = new Vector2(0, 0);
    }
}

//stop状态
public class StopState : FSMState
{
    Animator animator;
    public StopState(Animator anim)
    {
        stateID = StateID.Stop;
        animator = anim;
    }

    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        if(animator != null)
        {
            //Debug.Log("SetBool");
            animator.SetBool("stop", true);
        }
        Debug.Log("Enter Stop");

    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave Stop");
        if (animator != null)
        {
            animator.SetBool("stop", false);
        }
    }

    public override void ReState(GameObject player, GameObject enemy)
    {

    }

    public override void Update(GameObject player, GameObject enemy)
    {
        float x = 0.001f;
        if (IsEnemyLeft(player, enemy)) x = -x;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(x, 0);
    }
}

public class CheckPointState : FSMState
{
    float speed;
    Transform transform;

    public CheckPointState(float checkspeed)
    {
        speed = checkspeed;
        stateID = StateID.CheckPoint;
    }

    public void SetTransform(Transform trans)
    {
        transform = trans;
    }

    public override void DoBeforeEntering(GameObject player, GameObject enemy)
    {
        Debug.Log("Enter CheckPointState");
    }

    public override void DoBeforeLeaving(GameObject player, GameObject enemy)
    {
        Debug.Log("Leave CheckPointState");
        transform = null;
    }

    public override void ReState(GameObject player, GameObject enemy)
    {

    }

    public override void Update(GameObject player, GameObject enemy)
    {
        if(transform != null)
        {
            Vector3 moveDir = transform.position - enemy.transform.position;
            if (moveDir.magnitude < 0.6)
            {
                
                enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.ShouldStop, player, enemy);
            }
            else
            {
                moveDir = new Vector3(moveDir.x, 0, 0);
                LookAtDirection(player, enemy, moveDir);
                Vector3 vel = moveDir.normalized * speed;
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, 0);
            }
        }
    }
}