using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
