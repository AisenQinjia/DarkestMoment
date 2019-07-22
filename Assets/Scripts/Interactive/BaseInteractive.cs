using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseInteractive : MonoBehaviour
{
    protected Material shineMaterial;
    protected Material originMaterial;
    protected SpriteRenderer sr;
    public void Awake()
    {
        this.shineMaterial = Resources.Load<Material>(GameDefine.shineMaterial);
    }

    public void Start()
    {
        EventCenter.AddListener(EventType.OnChangeToPower, this.ChangeShineMaterial);
        EventCenter.AddListener(EventType.OnChangeToNormal, this.ChangeToOriginMaterial);
        Debug.Log("listen ");
    }

    protected void ChangeShineMaterial()
    {
        this.sr.material = this.shineMaterial;
    }

    protected void ChangeToOriginMaterial()
    {
        this.sr.material = this.originMaterial;
    }
    public virtual void InteractiveLogic(Transform player)
    {

    }

    public virtual void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.OnChangeToPower, this.ChangeShineMaterial);
        EventCenter.RemoveListener(EventType.OnChangeToNormal, this.ChangeToOriginMaterial);
    }

}
