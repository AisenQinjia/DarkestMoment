using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour
{
    string check_tag;
    bool is_play_music = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
        {
            check_tag = other.collider.tag;
            if (check_tag == GameDefine.PlayerTag)
            {
                Debug.Log("Play stone roll die");
                BaseRoleController control_dead = other.collider.GetComponent<BaseRoleController>();
                control_dead.OnDead();
            }
            if (check_tag == GameDefine.EnemyTag)
            {
                Debug.Log("Enemy stone roll die");
                BaseRoleController control_dead = other.collider.GetComponentInParent<BaseRoleController>();
                control_dead.OnDead();
            }
        }
    }
}
