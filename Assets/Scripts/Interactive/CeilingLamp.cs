using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : BaseInteractive
{
    Rigidbody2D ceiling_lamp;
    string check_tag;
    Rigidbody2D check_velocity;
    public int gravity_scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        ceiling_lamp.gravityScale = gravity_scale;
    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log("Ceiling lamp fall");
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.WakeUp();
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        check_velocity = other.rigidbody;
        check_tag = other.gameObject.tag;
        if (check_velocity.velocity[1] > 0)
        {
            if (check_tag == GameDefine.PlayerTag)
            {
                Debug.Log("Player ceiling touch die");
                BaseRoleController control_dead = other.collider.GetComponent<BaseRoleController>();
                control_dead.OnDead();
            }
            if (check_tag == GameDefine.EnemyTag)
            {
                Debug.Log("Enemy ceiling touch die");
                BaseRoleController control_dead = other.collider.GetComponentInParent<BaseRoleController>();
                control_dead.OnDead();
            }
        }
    }
}
