using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateBtn : JoystickBtn
{
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        EventCenter.Broadcast(EventType.OnChangeStateBtnClick);

    }
}
