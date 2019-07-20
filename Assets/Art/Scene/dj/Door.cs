using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0.3f);
        this.GetComponent<Collider2D>().enabled = false;
    }
}
