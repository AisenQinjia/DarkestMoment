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
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player open the unlock door");
            anim = GetComponent<Animator>();
            gameObject.GetComponent<Collider2D>().enabled = false;
            anim.SetFloat("Blend", 0.3f);
        }
        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy open the unlock door");
            anim = GetComponent<Animator>();
            gameObject.GetComponentInParent<Collider2D>().enabled = false;
            anim.SetFloat("Blend", 0.3f);
        }
    }
}
