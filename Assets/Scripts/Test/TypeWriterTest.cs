using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWriterTest : MonoBehaviour
{
    public GameObject player;
    void Awake()
    {
       
    }
    public string[] strs;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {

            //UIManager.Instance.PopTypewriterSentence("这是什么？", 0.1f, player.transform);
            UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 1f, player.transform);
            
            gameObject.SetActive(false);
        }
    }
}
