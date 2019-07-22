using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestInterative : BaseInteractive
{
    public override void InteractiveLogic(Transform player)
    {
        this.KillTween();
        this.transform.DOMoveY(-10, 0.5f);
    }
}
