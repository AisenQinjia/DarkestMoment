using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class JoystickInput : MonoBehaviour
{

    private Button jumpBtn;
    private Button killBtn;
    private Button interativeBtn;
    private Button powerStateBtn;
    private Button flowStateBtn;
    private Button stickStateBtn;
    private Button skillOneBtn;
    private Button skillTwoBtn;


    private void Awake()
    {

        this.jumpBtn = transform.Find("jumpBtn").GetComponent<Button>();
        this.killBtn = transform.Find("killBtn").GetComponent<Button>();
        //    this.interativeBtn = transform.Find("interativeBtn").GetComponent<Button>();
        this.powerStateBtn = transform.Find("powerStateBtn").GetComponent<Button>();
        this.flowStateBtn = transform.Find("flowStateBtn").GetComponent<Button>();
        this.stickStateBtn = transform.Find("stickStateBtn").GetComponent<Button>();
        this.skillOneBtn = transform.Find("skillOneBtn").GetComponent<Button>();
        this.skillTwoBtn = transform.Find("skillTwoBtn").GetComponent<Button>();

        this.jumpBtn.onClick.AddListener(OnJumpBtnClick);
        this.killBtn.onClick.AddListener(OnKillBtnClick);
        //   this.interativeBtn.onClick.AddListener(OnInteractiveBtnClick);
        this.powerStateBtn.onClick.AddListener(OnPowerStateBtnClick);
        this.flowStateBtn.onClick.AddListener(OnFlowStateBtnClick);
        this.stickStateBtn.onClick.AddListener(OnStickStateBtnClick);

        this.skillOneBtn.onClick.AddListener(OnSkillOneBtnClick);
        this.skillTwoBtn.onClick.AddListener(OnSkillTwoBtnClick);

    }

    private void OnDestroy()
    {
        this.jumpBtn.onClick.RemoveListener(OnJumpBtnClick);
        this.killBtn.onClick.RemoveListener(OnKillBtnClick);
        // this.interativeBtn.onClick.RemoveListener(OnInteractiveBtnClick);
        this.powerStateBtn.onClick.RemoveListener(OnPowerStateBtnClick);
        this.flowStateBtn.onClick.RemoveListener(OnFlowStateBtnClick);
        this.stickStateBtn.onClick.RemoveListener(OnStickStateBtnClick);

        this.skillOneBtn.onClick.RemoveListener(OnSkillOneBtnClick);
        this.skillTwoBtn.onClick.RemoveListener(OnSkillTwoBtnClick);

    }


    private void ScaleBtn(Transform t)
    {
        t.DOScale(new Vector2(1.2f, 1.2f), 0.1f).OnComplete(() =>
        {
            t.DOScale(new Vector2(1, 1), 0.1f);
        });
    }



    private void OnJumpBtnClick()
    {
        EventCenter.Broadcast(EventType.OnJumpBtnClick);
        ScaleBtn(this.jumpBtn.transform);
    }

    private void OnKillBtnClick()
    {
        EventCenter.Broadcast(EventType.OnKillBtnClick);
        ScaleBtn(this.killBtn.transform);
    }

    private void OnInteractiveBtnClick()
    {
        EventCenter.Broadcast(EventType.OnInterativeBtnClick);
        ScaleBtn(this.interativeBtn.transform);
    }

    private void OnStickStateBtnClick()
    {
        Debug.Log("click " + (int)PlayerState.Stick);
        EventCenter.Broadcast<int>(EventType.OnStickStateBtnClick, (int)PlayerState.Stick);
        ScaleBtn(this.stickStateBtn.transform);
    }

    private void OnFlowStateBtnClick()
    {
        Debug.Log("click " + (int)PlayerState.Flow);
        EventCenter.Broadcast<int>(EventType.OnFlowStateBtnClick, (int)PlayerState.Flow);
        ScaleBtn(this.flowStateBtn.transform);
    }

    private void OnPowerStateBtnClick()
    {
        Debug.Log("click " + (int)PlayerState.Power);
        EventCenter.Broadcast<int>(EventType.OnPowerStateBtnClick, (int)PlayerState.Power);
        ScaleBtn(this.powerStateBtn.transform);
    }




    private void OnSkillOneBtnClick()
    {
        EventCenter.Broadcast(EventType.OnSkillOneBtnClick);
        ScaleBtn(this.skillOneBtn.transform);
    }

    private void OnSkillTwoBtnClick()
    {
        EventCenter.Broadcast(EventType.OnSkillTwoBtnClick);
        ScaleBtn(this.skillTwoBtn.transform);
    }

}
