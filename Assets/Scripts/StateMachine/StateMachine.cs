﻿//状态机相关代码
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
    CheckPoint
}

//过渡
public enum Transition
{
    NullTransition,
    ShouldTurn,
    ShouldWalk,
    SawPlayer,
    LostPlayer,
    HeardNoise
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
        Debug.Log("AddState");
        if (state == null)
        {
            Debug.LogError("添加的状态机不存在");
            return;
        }
        if(states.Count == 0)
        {
            Debug.Log("states.Count == 0");
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
        moveDir = new Vector3(moveDir.x, 0, 0);
        if (moveDir.magnitude < 1)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Length)
            {
                currentPointIndex = 0;
            }
            enemy.GetComponent<PatrolEnemyController>().PerformTransition(Transition.ShouldTurn);
        }
        else
        {
            Vector3 vel = moveDir.normalized * enemy.GetComponent<PatrolEnemyController>().walkSpeed;

            // Rotate towards the waypoint
            //enemy.transform
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
        enemy.GetComponent<PatrolEnemyController>().PerformTransition(Transition.ShouldWalk);
    }
}

//追击
public class ChaseState : FSMState
{

}

//攻击
public class AttackState : FSMState
{

}

//后退
public class RetreatState : FSMState
{

}