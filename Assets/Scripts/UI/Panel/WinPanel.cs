using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    private Button restartBtn;
    private Button quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        this.restartBtn = this.transform.Find("restartBtn").GetComponent<Button>();
        this.quitBtn = this.transform.Find("quitBtn").GetComponent<Button>();

        this.restartBtn.onClick.AddListener(OnRestartClick);
        this.quitBtn.onClick.AddListener(OnQuitClick);
    }

    private void OnRestartClick()
    {
        Debug.Log("restarty");
        SceneManager.LoadScene(GameDefine.StartScene);
        UIManager.Instance.ClosePanel(GameDefine.winPanel);
    }

    private void OnQuitClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.winPanel);
        Application.Quit();
    }
}
