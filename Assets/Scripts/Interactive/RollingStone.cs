using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
    string check_tag;
    bool is_play_music = true;
    Rigidbody2D rigid2;
    Vector2 check_velcity;

    // Start is called before the first frame update
    void Start()
    {
        rigid2 = GetComponent<Rigidbody2D>();
        rigid2.isKinematic = true;
        check_velcity = new Vector2(0, 0);
    }

    void Update()
    {
        if (rigid2.velocity == check_velcity)
        {
            rigid2.isKinematic = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!rigid2.isKinematic)
        {
            check_tag = other.tag;
            if (check_tag == GameDefine.PlayerTag)
            {
                Debug.Log("Play stone roll die");
                BaseRoleController control_dead = other.GetComponent<BaseRoleController>();
                control_dead.OnDead();
            }
            if (check_tag == GameDefine.EnemyTag)
            {
                Debug.Log("Enemy stone roll die");
                BaseRoleController control_dead = other.GetComponentInParent<BaseRoleController>();
                control_dead.OnDead();
            }
        }
        
    }
}
