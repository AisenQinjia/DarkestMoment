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
    Retreat
}

//过渡
public enum Transition
{
    NullTransition,
    SawPlayer,
    LostPlayer
}

public class EnemyStateManager
{
    private List<FSMState> states;
    private StateID realCurrentStateID;
    public StateID currentStateID {
        set { realCurrentStateID = value; }
        get { return currentStateID; }
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
                Debug.LogError("FSM ERROR: 无法添加状态 " + state.stateID.ToString() + " 因为该状态已存在");
                return;
            }
        }
        states.Add(state);
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
    private float startPoint;
    private float endPoint;

    public WalkStateForPatrol(float startp, float endp)
    {
        startPoint = startp;
        endPoint   = endp;
        stateID = StateID.Walk;
    }
    public override void DoBeforeEntering() { }

    public override void DoBeforeLeaving() { }

    public override void ReState(GameObject player, GameObject enemy)
    {

    }

    public override void Update(GameObject player, GameObject enemy)
    {

    }
}


//只有转向敌人行走状态
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