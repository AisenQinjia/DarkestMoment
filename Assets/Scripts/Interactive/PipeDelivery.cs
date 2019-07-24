using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDelivery : BaseInteractive
{
    public GameObject other_pipe;
    // Start is called before the first frame update
    private float canEnterDis = 0.5f;//可进入水管距离

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
        if (Vector3.Distance(player.transform.position, this.transform.position) <= canEnterDis)
        {
            Debug.Log(player.name + " pipe delivery");
            player.position = other_pipe.transform.position;

        }
        else
        {
            UIManager.Instance.PopHint("水管要靠近才能互动。");
        }
    }
}
