using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : BaseInteractive
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
    }

    public override void InteractiveLogic(Transform player)
    {
        Debug.Log("player destroy the wall");
        AudioManager.Instance.PlayClip("墙壁裂开1");
        Destroy(gameObject);
    }
}
