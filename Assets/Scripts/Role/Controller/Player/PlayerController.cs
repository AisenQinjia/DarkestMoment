using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class PlayerController : BaseRoleController
{
    private GameObject stickState;
    private Animator curAnim;
    private Animator stickAnim;

    private RoleData roleData = new RoleData();

    private bool grounded = true;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody2D rb;
    public override void Awake()
    {
        base.Awake();
        RoleManager.Instance.AddPlayer(this.transform);
        stickState = transform.Find("stickState").gameObject;
        this.stickAnim = this.stickState.GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();

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
        Debug.Log(col);
        if (col.transform.CompareTag("Ground"))
        {
            this.grounded = true;
            Debug.Log(this.grounded);
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

        this.velocity.x = -1;
        this.stickAnim.SetBool("walk", true);

    }

    private void MoveRight()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        this.velocity.x = 1;
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
 
    }

    private void Interactive()
    {

    }

    public void Dead()
    {

    }
}
