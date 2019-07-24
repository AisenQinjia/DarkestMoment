using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHint : MonoBehaviour
{
    public string hint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            UIManager.Instance.PopHint(hint);
            gameObject.SetActive(false);
        }
    }
}
