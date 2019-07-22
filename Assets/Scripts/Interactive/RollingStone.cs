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
        if (is_play_music)
        {
            if (GetComponent<Rigidbody2D>().angularVelocity != 0)
            {
                AudioManager.Instance.PlayClip("stone_roll");
                is_play_music = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (GetComponent<Rigidbody2D>().angularVelocity != 0)
        {
            check_tag = other.gameObject.tag;
            if (check_tag == GameDefine.PlayerTag)
            {
                Debug.Log("Play touch die");
                BaseRoleController control_dead = other.collider.GetComponent<BaseRoleController>();
                control_dead.OnDead();
            }
            if (check_tag == GameDefine.EnemyTag)
            {
                Debug.Log("Enemy touch die");
                BaseRoleController control_dead = other.collider.GetComponentInParent<BaseRoleController>();
                control_dead.OnDead();
            }
        }
    }
}
