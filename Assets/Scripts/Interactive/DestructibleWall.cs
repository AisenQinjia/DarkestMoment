using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : BaseInteractive
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void InteractiveLogic(Transform player)
    {
        Destroy(gameObject);
    }
}
