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
    void OnTriggerEnter(Collider other)
    {
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag || check_tag == GameDefine.EnemyTag)
        {
            anim = GetComponent<Animator>();
            gameObject.GetComponent<Collider>().enabled = false;
            anim.SetFloat("Blend", 0.3f);
        }
    }
}
