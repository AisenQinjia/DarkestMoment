using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWriterTest : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            UIManager.Instance.PopTypewriterSentence("我擦！，碰到了什么狗东西？", 0.1f);
            UIManager.Instance.PopHint("PopHint");
            //gameObject.SetActive(false);
        }
    }
}
