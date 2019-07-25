using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBgm : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("endBgmContact");
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            AudioManager.Instance.StopClip("bgm");
            AudioManager.Instance.PlayClip("endbgm");
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
