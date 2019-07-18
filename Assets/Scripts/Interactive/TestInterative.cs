using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestInterative : BaseInteractive
{
    public override void InteractiveLogic(Transform player)
    {
        Debug.Log(123);
        this.transform.DOMoveY(-10, 0.5f);
    }
}
