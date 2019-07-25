using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StoryOpen : MonoBehaviour
{
    private string story = "欢迎来到我的世界，亲爱的\n你你，你是谁？\n你不需要知道我是谁，活下去吧,睁大你的眼睛看清周围，\n吞噬黑暗，找到我，我会告诉你的，千万别死了。 ";

    private Text text;
    private GameObject tips;

    private float wordTime = 0.05f;
    private void Awake()
    {
        this.text = this.transform.Find("text").GetComponent<Text>();
        this.tips = this.transform.Find("tips").gameObject;
    }



    private void Start()
    {
        AudioManager.Instance.PlayClip(GameDefine.typewriter, true);
        ShowSentence();
    }

    public void ShowSentence()
    {
        StartCoroutine(TypeString());
    }


    IEnumerator TypeString()
    {
        string tmp = "";
        for (int i = 0; i < story.Length; ++i)
        {
            tmp += story[i];
            text.text = tmp;
            yield return new WaitForSeconds(wordTime);
        }

        AudioManager.Instance.StopClip(GameDefine.typewriter);
        this.tips.SetActive(true);
        yield return new WaitForSeconds(5);
        StopAllCoroutines();

        SceneManager.LoadScene(GameDefine.StartScene);
    }

}
