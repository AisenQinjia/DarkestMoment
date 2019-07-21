using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class ItemData : IcfgId
{

    public int cfgId { get; set; }
    public string desc { get; set; }
    public string name { get; set; }
    public bool usable { get; set; }

    public string icon { get; set; }

    [JsonIgnore]
    public int count;

    public ItemData(ItemData data)
    {
        if (data != null)
        {
            this.cfgId = data.cfgId;
            this.desc = data.desc;
            this.name = data.name;
            this.usable = data.usable;
            this.icon = data.icon;
        }
        else
        {
            this.cfgId = -1;
            this.desc = "";
            this.name = "";
            this.usable = false;
            this.icon = "";
        }
        this.count = 0;
    }


}
