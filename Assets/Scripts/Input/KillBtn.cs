using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBtn : JoystickBtn
{

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClip("playerJump");
        EventCenter.Broadcast(EventType.OnKillBtnClick);
    }


}
