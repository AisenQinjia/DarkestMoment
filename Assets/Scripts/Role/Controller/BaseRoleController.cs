﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class BaseRoleController : MonoBehaviour
{
    public EnemyStateManager Statemanager;//我为什么要大写...
    public Rigidbody2D rigidbody;
    protected CharacterController cc;
    protected Animator anim;

    public virtual void Awake()
    {
        cc = this.GetComponent<CharacterController>();
  
        RoleManager.Instance.AddRole(this.transform);
    }

    public virtual void OnDestroy()
    {
        RoleManager.Instance.RemoveRole(this.transform);
        if (this.transform.CompareTag("Player"))
        {
            RoleManager.Instance.RemovePlayer();
        }
    }

    //角色死亡
    public virtual void OnDead()
    {

    }

    //吸引敌人注意力
    public virtual void ComeToMe(Transform trans)
    {

    }

    //判断player是否在敌人前面
    protected bool IsInForntOfEnemy(GameObject player, GameObject enemy)
    {
        Vector3 dir = player.transform.position - enemy.transform.position;
        return Mathf.Abs(Vector3.Angle(enemy.transform.right, dir)) < 90;
    }

    //
    public virtual void EnemyTurn()
    {

    }
}
