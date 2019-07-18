using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongPressBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public int type = 1;  //1 left 2 right
    private bool pressed = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.pressed = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        this.pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.pressed = false;
        Debug.Log(this.type + "btn up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.pressed = false;
        Debug.Log(this.type + "btn up");
    }

    public bool GetPressedState()
    {
        return this.pressed;
    }

    private void Update()
    {
        if (this.pressed)
        {
            //  Debug.Log(this.type + "btn pressed");
        }
    }

    private void SendPressedEvent()
    {
        if (this.type == 1)
        {
            this.SendLeftPressed();
        }
        else if (this.type == 2)
        {
            this.SendRightPressed();
        }
    }
    private void SendLeftPressed()
    {
        EventCenter.Broadcast(EventType.OnLeftBtnPressed);
    }

    private void SendRightPressed()
    {
        EventCenter.Broadcast(EventType.OnRightBtnPressed);
    }
}
