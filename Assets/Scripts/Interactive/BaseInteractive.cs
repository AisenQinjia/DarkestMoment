using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BaseInteractive : MonoBehaviour
{
    protected Material shineMaterial;
    protected Material originMaterial;
    protected SpriteRenderer sr;
    protected Vector3 scaleSize;
    protected Vector3 originScale;
    Tween tween;
    public virtual void Awake()
    {
        scaleSize = new Vector3(1.2f, 1.2f, 1.2f);
        originScale = this.transform.localScale;
        this.sr = this.GetComponent<SpriteRenderer>();
        this.originMaterial = this.sr.material;
        this.shineMaterial = Resources.Load<Material>(GameDefine.shineMaterial);

    }

    public virtual void Start()
    {
        EventCenter.AddListener(EventType.OnChangeToPower, this.ChangeShineMaterial);
        EventCenter.AddListener(EventType.OnChangeToNormal, this.ChangeToOriginMaterial);
    }

    protected void ChangeShineMaterial()
    {
        //int range = ConfigManager.Instance.GetRoleData(2).interativeRange;
        //if (Vector3.Distance(this.transform.position, RoleManager.Instance.transform.position) > range)
        //{
        //    Debug.Log(Vector3.Distance(this.transform.position, RoleManager.Instance.transform.position));
        //    return;
        //}
        tween = this.transform.DOScale(this.originScale * 1.2f, 1f);
        tween.SetLoops(-1);
        this.sr.material = this.shineMaterial;
    }

    protected void ChangeToOriginMaterial()
    {
        tween.Kill();
        this.transform.localScale = this.originScale;
        this.sr.material = this.originMaterial;
    }
    public virtual void InteractiveLogic(Transform player)
    {

    }

    protected void KillTween()
    {
        if (this.tween != null)
        {
            this.tween.Kill();
        }
    }

    public virtual void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.OnChangeToPower, this.ChangeShineMaterial);
        EventCenter.RemoveListener(EventType.OnChangeToNormal, this.ChangeToOriginMaterial);
    }



}
