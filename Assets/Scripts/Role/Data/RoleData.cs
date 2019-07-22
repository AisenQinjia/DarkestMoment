using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class RoleData : IcfgId
{
    public int cfgId { get; set; }
    public string name;
    public int walkSpeed;
    public int jumpForce;
    public float eatLong;
    public float eatWidth;
    public int interativeRange;
    public int viewRadius;
    public RoleData()
    {
        this.walkSpeed = 1;
        this.jumpForce = 300;
        this.eatLong = 1;
        this.eatWidth = 1;
        this.interativeRange = 1;
       
    }
}
