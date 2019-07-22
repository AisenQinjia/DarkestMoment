using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDelivery : BaseInteractive
{
    public GameObject other_pipe;
    // Start is called before the first frame update


    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        gameObject.tag = GameDefine.InterativeTag;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void InteractiveLogic(Transform player)
    {
        this.KillTween();
        Debug.Log(player.name + " pipe delivery");
        player.position = other_pipe.transform.position;
    }
}
