using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : BaseInteractive
{
    public GameObject game2;
    Rigidbody2D rigid2;
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
        rigid2 = game2.GetComponent<Rigidbody2D>();

        base.Start();
    }

    public override void InteractiveLogic(Transform player)
    {
        this.KillTween();
        Debug.Log("player destroy the wall");
        AudioManager.Instance.PlayClip("wall_break");
        AudioManager.Instance.PlayClip("stone_roll");
        rigid2.isKinematic = false;
        game2.GetComponent<RollingStone>().is_isk = true;
        
        Destroy(gameObject);
    }
}
