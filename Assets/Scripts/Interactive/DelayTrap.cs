using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTrap : BaseInteractive
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0.3f);
        control_dead.OnDead();
    }
}
