using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : BaseInteractive
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
        base.Start();
    }

    public override void InteractiveLogic(Transform player)
    {
        this.KillTween();
        Debug.Log("player destroy the wall");
        AudioManager.Instance.PlayClip("wall_break");
        Destroy(gameObject);
    }
}
