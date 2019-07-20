using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : BaseInteractive
{
    Rigidbody2D ceiling_lamp;
    string check_tag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void InteractiveLogic(Transform player)
    {
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag || check_tag == GameDefine.EnemyTag)
        {
            BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
            control_dead.OnDead();
        }
        
    }
}
