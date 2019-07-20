using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator animi;

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
        animi = GetComponent<Animator>();
        animi.SetBool("trap",false);
        this.GetComponent<Collider2D>().enabled = false;
    }
}
