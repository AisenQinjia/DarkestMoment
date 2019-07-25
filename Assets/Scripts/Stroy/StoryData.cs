using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class StoryData : IcfgId
{
    public int cfgId { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
}
