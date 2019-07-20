using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDelivery : BaseInteractive
{
    public GameObject other_pipe;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log(player.name);
        player.position = other_pipe.transform.position;
    }
}
