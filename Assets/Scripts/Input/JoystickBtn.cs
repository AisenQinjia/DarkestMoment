using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class JoystickBtn : MonoBehaviour, IPointerDownHandler
{

    protected JoystickInput joyStickInput;

    public virtual void Start()
    {
        joyStickInput = this.transform.GetComponentInParent<JoystickInput>();
        joyStickInput.AddBtn(this);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        this.ScaleBtn();
    }

    protected void ScaleBtn()
    {
        this.transform.DOScale(new Vector2(1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            this.transform.DOScale(new Vector2(1, 1), 0.1f);
        });
    }




}
