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
    public int eatLong;
    public int eatWidth;
    public int interativeRange;
    public RoleData()
    {
        this.walkSpeed = 1;
        this.jumpForce = 300;
        this.eatLong = 1;
        this.eatWidth = 1;
        this.interativeRange = 1;
    }
}
