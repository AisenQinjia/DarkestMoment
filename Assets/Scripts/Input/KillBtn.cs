using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBtn : JoystickBtn
{

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        EventCenter.Broadcast(EventType.OnKillBtnClick);
    }


}
