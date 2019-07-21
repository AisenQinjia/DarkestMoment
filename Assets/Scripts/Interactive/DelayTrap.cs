using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTrap : BaseInteractive
{
    Animator anim;
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
            Debug.Log("Player trap die");
            BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
            anim = GetComponent<Animator>();
            control_dead.OnDead();
            anim.SetFloat("Blend", 0.3f);
            
        }
        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy trap die");
            BaseRoleController control_dead = other.GetComponentInParent<BaseRoleController>();
            anim = GetComponent<Animator>();
            control_dead.OnDead();
            anim.SetFloat("Blend", 0.3f);
        }
    }
}
