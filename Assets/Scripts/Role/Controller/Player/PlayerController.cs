using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class PlayerController : BaseRoleController
{
    private GameObject stickState;
    private Animator curAnim;
    private Animator stickAnim;

    private RoleData roleData;

    private bool grounded = true;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody2D rb;

    private Vector2 attackBoxCenter;
    private Vector2 attackBoxSize;
    private Vector2 attackBoxDir;
    public override void Awake()
    {
        base.Awake();
        RoleManager.Instance.AddPlayer(this.transform);
        stickState = transform.Find("stickState").gameObject;
        this.stickAnim = this.stickState.GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
        this.roleData = ConfigManager.Instance.GetRoleData(1);

        this.attackBoxSize = new Vector2(this.roleData.eatLong, this.roleData.eatWidth);
        this.attackBoxDir = new Vector2(0, 0);

        EventCenter.AddListener(EventType.OnLeftBtnPressed, MoveLeft);
        EventCenter.AddListener(EventType.OnRightBtnPressed, MoveRight);
        EventCenter.AddListener(EventType.OnJumpBtnClick, Jump);
        EventCenter.AddListener(EventType.OnKillBtnClick, Eat);
        EventCenter.AddListener(EventType.OnInterativeBtnClick, Interactive);
        EventCenter.AddListener(EventType.PlayerStopWalk, StopWalk);

    }

    public override void OnDestroy()
    {
        base.OnDestroy();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }



        if (this.velocity.x != 0)
        {
            this.transform.position += velocity * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            this.grounded = true;
        }
    }

    public void ChangeState()
    {

    }

    private void StopWalk()
    {
        this.velocity.x = 0;
        this.stickAnim.SetBool("walk", false);
    }
    private void MoveLeft()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        this.velocity.x = -1 * this.roleData.walkSpeed;
        this.stickAnim.SetBool("walk", true);

    }

    private void MoveRight()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        this.velocity.x = 1*this.roleData.walkSpeed;
        this.stickAnim.SetBool("walk", true);
    }

    private void Jump()
    {
        if (this.grounded)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, this.roleData.jumpForce));
            this.grounded = false;
        }

    }

    private void Eat()
    {
        this.stickAnim.SetTrigger("eat");

        Debug.Log(this.transform.position);
        Debug.Log(this.transform.forward);
        Vector2 center = Vector2.zero;
        if (this.transform.rotation.y == 180)
        {
            center.x = this.transform.position.x + this.roleData.eatLong;
        }
        else
        {
            center.x = this.transform.position.x - this.roleData.eatWidth;
        }
        center.y = this.transform.position.y;

        // Debug.Log(center);

        RaycastHit2D hitInfo = Physics2D.BoxCast(center, this.attackBoxSize, 0, this.transform.forward, 10, LayerMask.GetMask("Enemy"));

        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                Debug.Log("eat enemy");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector2 center = Vector2.zero;
        if (this.transform.rotation.y == 180)
        {
            center.x = this.transform.position.x + this.transform.forward.z;
        }
        else
        {
            center.x = this.transform.position.x - this.transform.forward.z;
        }
        center.y = this.transform.position.y;
        Gizmos.DrawCube(center, this.attackBoxSize);
    }

    private void Interactive()
    {
        Vector2 center = Vector2.zero;
        if (this.transform.rotation.y == 180)
        {
            center.x = this.transform.position.x + this.roleData.eatLong;
        }
        else
        {
            center.x = this.transform.position.x - this.roleData.eatWidth;
        }
        center.y = this.transform.position.y;

        //Debug.Log(center);

        RaycastHit2D hitInfo = Physics2D.BoxCast(center, this.attackBoxSize, 0, this.transform.forward, 10, LayerMask.GetMask("Interative"));

        if (hitInfo)
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                Debug.Log("interactive obj");
            }
        }
    }

    public void Dead()
    {

    }


}
