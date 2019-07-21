using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class BaseRoleController : MonoBehaviour
{
    public EnemyStateManager Statemanager;//我为什么要大写...
    [HideInInspector]
    public Rigidbody2D rigidbody;
    protected CharacterController cc;
    protected Animator animator;

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
    public virtual void OnDead() { }

    //吸引敌人注意力
    public virtual void ComeToMe(Transform trans) { }

    //让敌人离开，恢复原来状态
    public virtual void LeaveMe() { }

    //离开walk状态
    public virtual void LeaveWalkState() { }

    //进入walk状态
    public virtual void ReturnToWalkState() { }

    //攻击
    public virtual void Attack() { }

    //等待时间
    public virtual void WaitTime(float time) { }


    //判断player是否在敌人前面
    protected bool IsInForntOfEnemy(GameObject player, GameObject enemy)
    {
        Vector3 dir = player.transform.position - enemy.transform.position;
        return Mathf.Abs(Vector3.Angle(enemy.transform.right, dir)) < 90;
    }

    //面对敌人
    public void LookAtPlayer(GameObject player, GameObject enemy)
    {
        Vector3 Dir = player.transform.position - enemy.transform.position;
        if (Mathf.Abs(Vector3.Angle(Dir, enemy.transform.right)) > 90)
        {
            enemy.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
