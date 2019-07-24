using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWriterTest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            UIManager.Instance.PopTypewriterSentence("我叼你妈的，我起了，一枪秒了，有什么好说的", 0.1f);
            gameObject.SetActive(false);
        }
    }
}
