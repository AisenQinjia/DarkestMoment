using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTouch : MonoBehaviour
{
    public int item_index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<PlayerController>().AddItem(item_index);
        Destroy(gameObject);
    }
    
    
}
