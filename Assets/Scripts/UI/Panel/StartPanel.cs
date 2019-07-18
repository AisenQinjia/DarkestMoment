using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    private Button StartBtn;
    private Button QuitBtn;

    public override void Awake()
    {
        StartBtn = this.transform.Find("StartBtn").GetComponent<Button>();
        QuitBtn = this.transform.Find("QuitBtn").GetComponent<Button>();

        StartBtn.onClick.AddListener(OnStartBtnClick);
        QuitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void OnStartBtnClick()
    {
        SceneManager.LoadScene(GameDefine.TestLevel);
    }

    private void OnQuitBtnClick()
    {
        Application.Quit();
    }

    public override void OnDestroy()
    {
        StartBtn.onClick.RemoveAllListeners();
        QuitBtn.onClick.RemoveAllListeners();
    }


}
