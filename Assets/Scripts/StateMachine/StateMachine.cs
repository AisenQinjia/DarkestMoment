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
    StareAtPlayer
}

//过渡
public enum Transition
{
    NullTransition,
    ShouldTurn,
    ShouldWalk,
    SawPlayer,
    CanChase,
    CanAttack,
    LostPlayer,
    HeardNoise,
    TouchedBarrier
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

    public void DeleteState(StateID id) { }

    public void PerformTransition(Transition trans)
    {
        if(trans == Transition.NullTransition)
        {
            Debug.LogError("trans is null!");
            return;
        }
        
        StateID id = currentState.GetState(trans);
        if (id == StateID.NullState)
        {
            Debug.Log("不存在目标状态");
            return;
        }

        currentStateID = id;
        foreach(FSMState st in states)
        {
            if (st.stateID == currentStateID)
            {
                currentState.DoBeforeLeaving();
                currentState = st;
                currentState.DoBeforeLeaving();
                break;
            }
        }
    }
}
//抽象基类
public abstract class FSMState
{
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    public StateID stateID;

    public virtual void DoBeforeEntering() { }

    public virtual void DoBeforeLeaving() { }
 
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
    public void LookAtDirection(GameObject enemy, Vector3 moveDir)
    {
        moveDir = new Vector3(moveDir.x, 0, 0);
        if (Mathf.Abs(Vector3.Angle(moveDir, enemy.transform.right)) > 90)
        {
            enemy.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    //背对着行进方向
    public void BackAtDirection(GameObject enemy, Vector3 moveDir)
    {
        moveDir = new Vector3(moveDir.x, 0, 0);
        if (Mathf.Abs(Vector3.Angle(moveDir, enemy.transform.right)) < 90)
        {
            enemy.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    //执行过渡
    public void PerformTransition(Transition trans, GameObject enemy)
    {
        enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(trans);
    }

    //计算是否在目标视野范围
    public bool IsInRange(GameObject player, GameObject enemy, float angle, float range)
    {
        Vector3 dir = player.transform.position - enemy.transform.position;
        return Mathf.Abs(Vector3.Angle(enemy.transform.right, dir)) < angle && dir.magnitude < range;
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
    public override void DoBeforeEntering() { }

    public override void DoBeforeLeaving() { }

    public override void ReState(GameObject player, GameObject enemy)
    {

    }

    public override void Update(GameObject player, GameObject enemy)
    {
        Vector3 moveDir = pathPoints[currentPointIndex].position - enemy.transform.position;
        
        if (moveDir.magnitude < 1)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Length)
            {
                currentPointIndex = 0;
            }
            enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.ShouldTurn);
        }
        else
        {
            moveDir = new Vector3(moveDir.x, 0, 0);
            LookAtDirection(enemy, moveDir);
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
    public override void ReState(GameObject player, GameObject enemy) { }

    public override void Update(GameObject player, GameObject enemy)
    {
        enemy.transform.Rotate(new Vector3(0, 180, 0));
        enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.ShouldWalk);
    }
}

//追击
public class ChaseState : FSMState
{
    private float speed;
    private float chaseRange;
    private float attackRange;

    public ChaseState(float vel, float chaserange, float attackrange)
    {
        stateID = StateID.Chase;
        speed = vel;
        chaseRange = chaserange;
        attackRange = attackrange;
    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        
    }

    public override void Update(GameObject player, GameObject enemy)
    {
        Vector3 moveDir = player.transform.position - enemy.transform.position;
        moveDir = new Vector3(moveDir.x, 0, 0);
        LookAtDirection(enemy, moveDir);
        if (moveDir.magnitude > chaseRange)
        {
            enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.LostPlayer);
        }
        else if(moveDir.magnitude < attackRange)
        {
            enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.CanAttack);
        }
        else
        {
            Vector3 vel = moveDir.normalized * speed;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(vel.x, 0);
        }
    }
}

//攻击
public class AttackState : FSMState
{
    public AttackState()
    {
        stateID = StateID.Attack;
    }

    public override void DoBeforeEntering()
    {
        
    }

    public override void DoBeforeLeaving()
    {
        
    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        Debug.LogError("Die !");
        enemy.GetComponent<BaseRoleController>().Statemanager.PerformTransition(Transition.LostPlayer);
    }
}

//后退
public class RetreatState : FSMState
{
    //就是巡逻的两个点
    Transform[] paths;
    float speed;
    float minX;
    float maxX;

    public RetreatState(Transform[] path, float vel)
    {
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
    public override void DoBeforeEntering()
    {

    }

    public override void DoBeforeLeaving()
    {

    }

    public override void ReState(GameObject player, GameObject enemy)
    {
        if(enemy.transform.position.x > minX && enemy.transform.position.x < maxX)
        {
            PerformTransition(Transition.ShouldWalk, enemy);
        }
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        if(enemy.transform.position.x < minX)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else if(enemy.transform.position.x > maxX)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
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

    public override void DoBeforeEntering()
    {
        timer = 0;
    }

    public override void DoBeforeLeaving()
    {
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
                PerformTransition(Transition.LostPlayer, enemy);
            }
        }
    }
    public override void Update(GameObject player, GameObject enemy)
    {
        timer = timer + Time.deltaTime;
        enemy.GetComponent<BaseRoleController>().rigidbody.velocity = new Vector2(0, 0);
    }

}