using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : BasePanel
{
    private Text name;
    private Text desc;
    private Image icon;

    private void Awake()
    {
        this.name = this.transform.Find("name").GetComponent<Text>();
        this.desc = this.transform.Find("desc").GetComponent<Text>();
        this.icon = this.transform.Find("icon").GetComponent<Image>();
    }
    public void Init(string name, string desc, string icon)
    {
        this.name.text = name;
        this.desc.text = desc;
        this.icon.sprite = Resources.Load(GameDefine.itemIconPath + this.icon, typeof(Sprite)) as Sprite;
    }


}
