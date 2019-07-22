using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBtn : JoystickBtn
{
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClip(GameDefine.playerEat);
        EventCenter.Broadcast(EventType.OnJumpBtnClick);
    }
}


