using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//巡逻型敌人
public class PatrolEnemyController : MonoBehaviour
{
    public GameObject player;
    public float walkSpeed;
    public float chaseSpeed;
    public float chaseRange;
    public float attackRange;
    public float retreatSpeed;
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
        walk.AddTransition(Transition.SawPlayer, StateID.Chase);

        TurnState turn = new TurnState();
        turn.AddTransition(Transition.ShouldWalk, StateID.Walk);

        ChaseState chase = new ChaseState(chaseSpeed, chaseRange, attackRange);
        chase.AddTransition(Transition.CanAttack, StateID.Attack);
        chase.AddTransition(Transition.LostPlayer, StateID.Retreat);

        AttackState attack = new AttackState();
        attack.AddTransition(Transition.LostPlayer, StateID.Retreat);

        RetreatState retreat = new RetreatState(path, retreatSpeed);
        retreat.AddTransition(Transition.ShouldWalk, StateID.Walk);
        retreat.AddTransition(Transition.SawPlayer, StateID.Chase);


        Statemanager.AddState(walk);
        Statemanager.AddState(turn);
        Statemanager.AddState(chase);
        Statemanager.AddState(attack);
        Statemanager.AddState(retreat);

    }

    public void PerformTransition(Transition trans)
    {
        Statemanager.PerformTransition(trans);
    }

    //进入视野
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player detected!");
            Statemanager.PerformTransition(Transition.SawPlayer);
        }
    }

    //死亡接口
    public void Dead()
    {
        Debug.LogError(" I am Dead!");
    }

    //吸引敌人注意力接口
    public void ComeToMe(Transform trans)
    {
        Statemanager.PerformTransition(Transition.HeardNoise);
    }
}
