using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    BaseRoleController enemy;
    void Start()
    {
        enemy = transform.GetComponentInParent<BaseRoleController>();
        if (enemy == null) Debug.LogError("enemy not found!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            Debug.Log("PlayerContact!");
            enemy.Attack();
            other.gameObject.GetComponent<BaseRoleController>().OnDead();
        }
    }
}
