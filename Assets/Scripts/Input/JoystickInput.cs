﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour
{

    private List<JoystickBtn> joystickBtns = new List<JoystickBtn>();
    private List<LongPressBtn> longPressBtns = new List<LongPressBtn>();

    private Button inventoryBtn;
    private GameObject leftBtnGo;
    private GameObject rightBtnGo;
    private GameObject killBtnGo;
    private GameObject jumpBtnGo;
    private void Awake()
    {
        inventoryBtn = transform.Find("inventoryBtn").GetComponent<Button>();
        leftBtnGo = transform.Find("leftBtn").gameObject;
        rightBtnGo = transform.Find("rightBtn").gameObject;
        killBtnGo = transform.Find("killBtn").gameObject;
        jumpBtnGo = transform.Find("jumpBtn").gameObject;

        EventCenter.AddListener(EventType.OnPlayerDead, DisableInput);
        EventCenter.AddListener(EventType.OnChangeToNormal, HidePowerInput);
        EventCenter.AddListener(EventType.OnChangeToPower, HideNormalInput);
        this.inventoryBtn.onClick.AddListener(OnInventoryBtnClick);
    }

    private void OnDestroy()
    {

        EventCenter.RemoveListener(EventType.OnPlayerDead, DisableInput);
        this.inventoryBtn.onClick.RemoveListener(OnInventoryBtnClick);
        EventCenter.RemoveListener(EventType.OnChangeToNormal, HidePowerInput);
        EventCenter.RemoveListener(EventType.OnChangeToPower, HideNormalInput);

        this.joystickBtns.Clear();
        this.longPressBtns.Clear();
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
                if (!EventSystem.current.IsPointerOverGameObject() && hitInfo.transform.CompareTag(GameDefine.InterativeTag))
                {
                    EventCenter.Broadcast<GameObject>(EventType.OnClickInteractive, hitInfo.transform.gameObject);
                }
            }
        }
    }

    private void OnInventoryBtnClick()
    {
        UIManager.Instance.PopPanel(GameDefine.inventoryPanel);
    }

    private void HidePowerInput()
    {
        this.leftBtnGo.SetActive(true);
        this.rightBtnGo.SetActive(true);
        this.killBtnGo.SetActive(true);
        this.jumpBtnGo.SetActive(true);
        for (int i = 0; i < this.longPressBtns.Count; i++)
        {
            this.longPressBtns[i].pressed = false;
        }
    }

    private void HideNormalInput()
    {
        this.leftBtnGo.SetActive(false);
        this.rightBtnGo.SetActive(false);
        this.killBtnGo.SetActive(false);
        this.jumpBtnGo.SetActive(false);
    }

}
