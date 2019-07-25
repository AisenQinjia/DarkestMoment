using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTypeWriter : MonoBehaviour
{
    public Transform position;
    public string[] strs;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameDefine.PlayerTag))
        {
            if(position != null)
            {
                UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 1f, position);
            }
            else UIManager.Instance.PopTypewriterSentences(strs, 0.1f, 1f, transform);
            gameObject.SetActive(false);
        }
    }
}
