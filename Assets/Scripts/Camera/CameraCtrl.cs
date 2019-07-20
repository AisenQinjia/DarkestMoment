using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    public float smooth = 0.1f;
    void Start()
    {

    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (RoleManager.Instance.GetPlayer() == null)
            return;

        Vector3 targetPos = new Vector3(RoleManager.Instance.playerTransform.position.x, RoleManager.Instance.playerTransform.position.y + 1, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, smooth);
    }
}
