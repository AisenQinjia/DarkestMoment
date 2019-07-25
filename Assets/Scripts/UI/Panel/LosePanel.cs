using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel
{
    private Button restartBtn;
    private Button quitBtn;
    private Button backBtn;
    // Start is called before the first frame update
    void Start()
    {
        this.restartBtn = this.transform.Find("restartBtn").GetComponent<Button>();
        this.quitBtn = this.transform.Find("quitBtn").GetComponent<Button>();
        this.backBtn = this.transform.Find("backBtn").GetComponent<Button>();


        this.restartBtn.onClick.AddListener(OnRestartClick);
        this.quitBtn.onClick.AddListener(OnQuitClick);
        this.backBtn.onClick.AddListener(OnBackBtnClick);
    }

    private void OnBackBtnClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.losePanel);
        GameManager.Instance.ReloadScene(GameDefine.StartScene);
    }
    private void OnRestartClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.losePanel);
        GameManager.Instance.RestartGame();
    }

    private void OnQuitClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.losePanel);
        Application.Quit();
    }
}
