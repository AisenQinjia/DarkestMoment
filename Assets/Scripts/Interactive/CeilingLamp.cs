using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : BaseInteractive
{
    Rigidbody2D ceiling_lamp;
    string check_tag;
    Rigidbody2D check_velocity;
    bool is_play_music = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.gravityScale = 0;
    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log("Ceiling lamp fall");
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.gravityScale = 1;
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (is_play_music)
        {
            AudioManager.Instance.PlayClip("ceiling_lamp_break");
            is_play_music = false;

            check_tag = other.collider.tag;
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
        
        /*
        check_velocity = GetComponent<Rigidbody2D>();
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
        */
    }
}
