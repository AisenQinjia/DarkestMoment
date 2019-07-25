using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StroyPanel : BasePanel
{

    private Button quitBtn;
    private Text title;
    private Text desc;

    public override void Awake()
    {
        this.quitBtn = this.transform.Find("quitBtn").GetComponent<Button>();
        this.title = this.transform.Find("title").GetComponent<Text>();
        this.desc = this.transform.Find("desc").GetComponent<Text>();
        this.quitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void OnQuitBtnClick()
    {
        UIManager.Instance.ClosePanel(GameDefine.StoryPanel);
    }



    public override void OnPop<T>(T cfgId)
    {
        Time.timeScale = 0;
        StoryData temp = ConfigManager.Instance.GetStoryData((int.Parse(cfgId.ToString())));
        this.title.text = temp.title;
        this.desc.text = temp.desc;

    }

    public override void OnClose()
    {
        Time.timeScale = 1;
    }

  



}
