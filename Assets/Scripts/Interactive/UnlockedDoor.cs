using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedDoor : BaseInteractive
{
    Animator anim;
    string check_tag;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player open the unlock door");
            AudioManager.Instance.PlayClip("开门");
            anim.SetFloat("Blend", 0.3f);
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy open the unlock door");
            AudioManager.Instance.PlayClip("开门");
            anim.SetFloat("Blend", 0.3f);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player close the unlock door");
            AudioManager.Instance.PlayClip("锁门");
            anim.SetFloat("Blend", -0.1f);
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy close the unlock door");
            AudioManager.Instance.PlayClip("锁门");
            anim.SetFloat("Blend", -0.1f);
        }

    }
}
