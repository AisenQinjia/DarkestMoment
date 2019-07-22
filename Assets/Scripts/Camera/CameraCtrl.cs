using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraCtrl : MonoBehaviour
{

    public float smooth = 0.1f;
    void Start()
    {
        EventCenter.AddListener(EventType.CameraShake, CameraShake);
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

    private void CameraShake()
    {
        Tween tween = this.transform.DOShakePosition(0.2f, 1f);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.CameraShake, CameraShake);
    }
}
