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

        Statemanager.AddState(walk);
    }

    public void PerformTransition(Transition trans)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
