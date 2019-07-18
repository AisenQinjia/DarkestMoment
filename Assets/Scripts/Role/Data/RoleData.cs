using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{
    [SerializeField]
    public int walkSpeed;
    public int jumpForce;
    public int eatLong;
    public int eatWidth;

    public RoleData()
    {
        this.walkSpeed = 1;
        this.jumpForce = 300;
        this.eatLong = 1;
        this.eatWidth = 1;
    }
}
