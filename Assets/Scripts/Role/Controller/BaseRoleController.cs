using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class BaseRoleController : MonoBehaviour
{

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
}
