using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private string desc;
    private string name;
    private bool usable;
    private string icon;
    private Image image;

    private void Awake()
    {
        this.image = this.GetComponent<Image>();
    }
    public void Init(ItemData data)
    {

        this.desc = data.desc;
        this.name = data.name;
        this.usable = data.usable;
        this.icon = data.icon;
        this.SetIcon();
    }

    private void SetIcon()
    {
        if (this.icon == "")
            return;
        image.sprite = Resources.Load(GameDefine.itemIconPath + this.icon, typeof(Sprite)) as Sprite;
    }


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!this.usable)
        {
            GameObject infoGo = UIManager.Instance.PopPanel(GameDefine.itemInfoUI);
            infoGo.GetComponent<ItemInfoUI>().Init(this.name, this.desc, this.icon);
        }
        //todo 实现使用
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        UIManager.Instance.ClosePanel(GameDefine.itemInfoUI);
    }
}
