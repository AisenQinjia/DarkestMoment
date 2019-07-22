using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraCtrl : MonoBehaviour
{

    public float smooth = 0.1f;

    private Light SpotLight;
    private bool changeView;
    private float targetView;
    public float changeViewSpeed = 60f;
    void Start()
    {
        EventCenter.AddListener(EventType.CameraShake, CameraShake);
        EventCenter.AddListener<float>(EventType.ChangeView, ChangeView);

        this.SpotLight = this.transform.Find("SpotLight").GetComponent<Light>();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.CameraShake, CameraShake);
        EventCenter.RemoveListener<float>(EventType.ChangeView, ChangeView);
    }

    void Update()
    {
        Follow();

        if (this.changeView)
        {

            this.SpotLight.spotAngle = Mathf.Lerp(this.SpotLight.spotAngle, this.targetView, changeViewSpeed * Time.deltaTime);
            Debug.Log("targetView " + this.targetView + " spotangle " + this.SpotLight.spotAngle);
            if (Mathf.Abs(this.SpotLight.spotAngle - this.targetView) < 1.5f)
                this.changeView = false;
        }
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



    private void ChangeView(float viewRadius)   //切换视野
    {
        this.changeView = true;
        this.targetView = viewRadius;
        //  this.SpotLight.spotAngle = viewRadius;
        //  this.SpotLight.spotAngle = Mathf.Lerp(this.SpotLight.spotAngle, this.targetView, 1f);
    }
}
