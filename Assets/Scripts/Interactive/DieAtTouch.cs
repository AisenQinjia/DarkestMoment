using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAtTouch : BaseInteractive
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
        control_dead.OnDead();
    }
}
