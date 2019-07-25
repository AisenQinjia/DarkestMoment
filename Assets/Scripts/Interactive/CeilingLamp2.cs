using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp2 : BaseInteractive
{
    Rigidbody2D ceiling_lamp;
    string check_tag;
    
    bool is_play_music = true;
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void Start()
    {
        base.Start();
        gameObject.tag = GameDefine.InterativeTag;
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.isKinematic = true;
        ceiling_lamp.gravityScale = 1;
    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log("Ceiling lamp fall");
        ceiling_lamp.isKinematic = false;

        this.KillTween();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (is_play_music && !ceiling_lamp.isKinematic)
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
        
    }
}

