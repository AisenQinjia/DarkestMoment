using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag(GameDefine.PlayerTag))
        {
            col.transform.GetComponent<PlayerController>().enabled = false;
            UIManager.Instance.PopPanel(GameDefine.winPanel);
        }
    }


}
