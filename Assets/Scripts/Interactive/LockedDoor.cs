﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : BaseInteractive
{
    Animator anim;
    string check_tag;
    public int key_index = 1;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("open door");
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag && other.gameObject.GetComponent<PlayerController>().HasItem(key_index))
        {
            Debug.Log("Player open the lock door");
            anim = GetComponent<Animator>();
            anim.SetFloat("Blend", 0.3f);
            this.col.enabled = false;
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy open the lock door");
            // anim = GetComponentInParent<Animator>();
            anim.SetFloat("Blend", 0.3f);

        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("close door");
        check_tag = other.gameObject.tag;
        if (check_tag == GameDefine.PlayerTag)
        {
            Debug.Log("Player close the lock door");
            anim = GetComponent<Animator>();
            anim.SetFloat("Blend", -0.1f);
            this.col.enabled = true;
        }

        if (check_tag == GameDefine.EnemyTag)
        {
            Debug.Log("Enemy close the lock door");
            // anim = GetComponentInParent<Animator>();
            anim.SetFloat("Blend", -0.1f);
            this.col.enabled = true;
        }

    }
}
