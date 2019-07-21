using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : BaseInteractive
{
    Rigidbody2D ceiling_lamp;
    string check_tag;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log("Ceiling lamp fall");
        ceiling_lamp = GetComponent<Rigidbody2D>();
        ceiling_lamp.gravityScale = 1;
    }

    
}
