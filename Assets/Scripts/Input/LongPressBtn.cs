using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LongPressBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public int type = 1;  //1 left 2 right
    private bool pressed = false;

    protected JoystickInput joyStickInput;

    public virtual void Start()
    {
        joyStickInput = this.transform.GetComponentInParent<JoystickInput>();
        joyStickInput.AddLongPressBtn(this);
    }
    private void ScaleBtn()
    {
        this.transform.DOScale(new Vector2(1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            transform.DOScale(new Vector2(1, 1), 0.1f);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaleBtn();
        this.pressed = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleBtn();
        this.pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.pressed = false;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.pressed = false;
        EventCenter.Broadcast(EventType.PlayerStopWalk);
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
            SendPressedEvent();
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
