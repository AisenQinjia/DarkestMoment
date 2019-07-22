using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JoystickInput : MonoBehaviour
{

    private List<JoystickBtn> joystickBtns = new List<JoystickBtn>();
    private List<LongPressBtn> longPressBtns = new List<LongPressBtn>();


    private void Awake()
    {

        EventCenter.AddListener(EventType.OnPlayerDead, DisableInput);

    }

    public void AddBtn(JoystickBtn btn)
    {
        this.joystickBtns.Add(btn);
        //  Debug.Log(this.joystickBtns.Count);
    }

    public void AddLongPressBtn(LongPressBtn btn)
    {
        this.longPressBtns.Add(btn);
    }
    private void OnDestroy()
    {

        EventCenter.RemoveListener(EventType.OnPlayerDead, DisableInput);
        this.joystickBtns.Clear();
        this.longPressBtns.Clear();
    }

    private void DisableInput()
    {
        for (int i = 0; i < this.joystickBtns.Count; i++)
        {
            this.joystickBtns[i].enabled = false;
        }

        for (int i = 0; i < this.longPressBtns.Count; i++)
        {
            this.longPressBtns[i].enabled = false;
        }
        this.enabled = false;
    }


    private RaycastHit2D hitInfo;
    private Vector2 rayOrigin;
    private void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {

            if (Application.isMobilePlatform)
                rayOrigin = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 15);
            if (hitInfo)
            {
                if (hitInfo.transform.CompareTag(GameDefine.InterativeTag))
                {
                    EventCenter.Broadcast<GameObject>(EventType.OnClickInteractive, hitInfo.transform.gameObject);
                }
            }
        }
    }





}
