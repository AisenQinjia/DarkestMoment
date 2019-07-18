using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//巡逻型敌人
public class PatrolEnemyController : MonoBehaviour
{
    public GameObject player;
    public float walkSpeed;
    public float chaseSpeed;
    public Transform[] path;
    private EnemyStateManager Statemanager;
    // Start is called before the first frame update
    void Start()
    {
        CreateFSM();
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

        TurnState turn = new TurnState();
        turn.AddTransition(Transition.ShouldWalk, StateID.Walk);

        Statemanager.AddState(walk);
        Statemanager.AddState(turn);
    }

    public void PerformTransition(Transition trans)
    {
        Statemanager.PerformTransition(trans);
    }

    //进入视野
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }

    //死亡接口
    public void Dead()
    {

    }

    //吸引敌人注意力接口
    public void ComeToMe(Transform trans)
    {

    }
}
