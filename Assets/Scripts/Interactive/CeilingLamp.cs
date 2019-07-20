using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : BaseInteractive
{
    Rigidbody ceiling_lamp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void InteractiveLogic(Transform player)
    {
        ceiling_lamp = GetComponent<Rigidbody>();
        ceiling_lamp.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
        control_dead.OnDead();
    }
}
