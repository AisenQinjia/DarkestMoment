﻿using System.Collections;
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
        Debug.Log("open door");
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player open the lock door");
            
            anim.SetFloat("Blend", 0.3f);
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy open the lock door");
            
            anim.SetFloat("Blend", 0.3f);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("close door");
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player close the lock door");
            
            anim.SetFloat("Blend", -0.1f);
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy close the lock door");
            
            anim.SetFloat("Blend", -0.1f);
        }

    }
}
