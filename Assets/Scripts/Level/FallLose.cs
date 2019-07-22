using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLose : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.transform.CompareTag(GameDefine.PlayerTag))
        {
            col.transform.GetComponent<BaseRoleController>().OnDead();
        }
    }
}
