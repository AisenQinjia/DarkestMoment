using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoUI : MonoBehaviour
{

    private Button backBtn;

    private void Awake()
    {
        this.backBtn = this.transform.Find("backBtn").GetComponent<Button>();
        this.backBtn.onClick.AddListener(OnBackBtnClick);
    }

    private void OnBackBtnClick()
    {
        GameManager.Instance.ReloadScene(GameDefine.StartScene);
    }
}
