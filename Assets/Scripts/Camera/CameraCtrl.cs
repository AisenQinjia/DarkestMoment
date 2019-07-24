using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraCtrl : MonoBehaviour
{

    public float smooth = 0.1f;
    public float changeViewSpeed = 60f;
    public int hightLighIntensity = 50;
    public float resumeIntensityTime = 0.5f;

    private Light SpotLight;
    private bool changeView;
    private float targetView;
    private float originIntensity;
    private Transform blood;
    void Start()
    {
        EventCenter.AddListener<float, float>(EventType.CameraShake, CameraShake);
        EventCenter.AddListener<float>(EventType.ChangeView, ChangeView);
        EventCenter.AddListener(EventType.CameraHightLight, HightLight);

        this.blood = transform.Find("blood");

        this.SpotLight = this.transform.Find("SpotLight").GetComponent<Light>();
        this.originIntensity = this.SpotLight.intensity;

    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float, float>(EventType.CameraShake, CameraShake);
        EventCenter.RemoveListener<float>(EventType.ChangeView, ChangeView);
        EventCenter.RemoveListener(EventType.CameraHightLight, HightLight);
    }

    void Update()
    {
        Follow();

        if (this.changeView)
        {

            this.SpotLight.spotAngle = Mathf.Lerp(this.SpotLight.spotAngle, this.targetView, changeViewSpeed * Time.deltaTime);
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

    private void CameraShake(float duration, float strength)
    {
        Tween tween = this.transform.DOShakePosition(duration, strength);
    }



    private void ChangeView(float viewRadius)   //切换视野
    {
        this.changeView = true;
        this.targetView = viewRadius;
        //  this.SpotLight.spotAngle = viewRadius;
        //  this.SpotLight.spotAngle = Mathf.Lerp(this.SpotLight.spotAngle, this.targetView, 1f);
    }

    private void HightLight()
    {
        this.SpotLight.intensity = hightLighIntensity;
        this.blood.gameObject.SetActive(true);
        UIManager.Instance.EnabaleBloodSprite();
        StartCoroutine(ResumeIntensity());
    }

    IEnumerator ResumeIntensity()
    {
        yield return new WaitForSeconds(resumeIntensityTime);
        this.SpotLight.intensity = this.originIntensity;
        this.blood.gameObject.SetActive(false);
        UIManager.Instance.DisableBloodSprite();

    }
}
