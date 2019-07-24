using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    private Button lv1Btn;
    private Button lv2Btn;
    private Button bossLevelBtn;
    private Button QuitBtn;

    public override void Awake()
    {
        lv1Btn = this.transform.Find("lv1Btn").GetComponent<Button>();
        lv2Btn = this.transform.Find("lv2Btn").GetComponent<Button>();
        bossLevelBtn = this.transform.Find("bossLevelBtn").GetComponent<Button>();
        QuitBtn = this.transform.Find("QuitBtn").GetComponent<Button>();

        lv1Btn.onClick.AddListener(OnLevelOneBtnClick);
        lv2Btn.onClick.AddListener(OnLevelTwoBtnClick);
        bossLevelBtn.onClick.AddListener(OnBossLevelClick);
        QuitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void OnLevelOneBtnClick()
    {
        GameManager.level = 1;
        GameManager.Instance.ReloadScene(GameDefine.LevelOne);
    }

    private void OnLevelTwoBtnClick()
    {
        GameManager.level = 2;
        GameManager.Instance.ReloadScene(GameDefine.LevelTwo);
    }

    private void OnBossLevelClick()
    {
        GameManager.level = 3;
        GameManager.Instance.ReloadScene(GameDefine.BossLevel);

    }
    private void OnQuitBtnClick()
    {
        Application.Quit();
    }

    public override void OnDestroy()
    {
        lv1Btn.onClick.RemoveAllListeners();
        lv2Btn.onClick.RemoveAllListeners();
        QuitBtn.onClick.RemoveAllListeners();
    }


}
