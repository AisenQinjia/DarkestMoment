using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAtTouch : BaseInteractive
{
    string check_tag;
    // Start is called before the first frame update
    void Start()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Play touch die");
            BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
            control_dead.OnDead();
        }
        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy touch die");
            BaseRoleController control_dead = other.GetComponentInParent<BaseRoleController>();
            control_dead.OnDead();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Play touch die");
            BaseRoleController control_dead = other.collider.GetComponent<BaseRoleController>();
            control_dead.OnDead();
        }
        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy touch die");
            BaseRoleController control_dead = other.collider.GetComponentInParent<BaseRoleController>();
            control_dead.OnDead();
        }
    }
}
