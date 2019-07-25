using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWife : MonoBehaviour
{
    string[] strs;
    void Start()
    {
        strs = new string[2];
        strs[0] = "非礼啊啊!";
        strs[1] = "老公救我!";
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {

            //UIManager.Instance.PopTypewriterSentence("这是什么？", 0.1f, player.transform);
            UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 0.5f, transform);
            PlayMovie();
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void PlayMovie()
    {

    }
}
