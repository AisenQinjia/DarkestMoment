using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState
{
    Stick = 0,
    Flow,
    Power,
}

public class PlayerController : BaseRoleController
{

    private GameObject[] stateGos = new GameObject[3];

    private Animator[] stateAnims = new Animator[3];

    private bool grounded = true;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Rigidbody2D rb;

    private Vector2 attackBoxCenter;
    private Vector2 attackBoxSize;
    private Vector2 attackBoxDir;

    private PlayerState state;
    private RoleData[] stateDatas = new RoleData[3];
    public override void Awake()
    {
        base.Awake();
        this.state = PlayerState.Stick;

        RoleManager.Instance.AddPlayer(this.transform);
        this.rb = GetComponent<Rigidbody2D>();

        stateGos[(int)PlayerState.Stick] = transform.Find("stickState").gameObject;
        stateGos[(int)PlayerState.Flow] = transform.Find("flowState").gameObject;
        stateGos[(int)PlayerState.Power] = transform.Find("powerState").gameObject;

        this.stateAnims[(int)PlayerState.Stick] = stateGos[(int)PlayerState.Stick].GetComponent<Animator>();


        this.stateDatas[(int)PlayerState.Stick] = ConfigManager.Instance.GetRoleData(1);
        this.stateDatas[(int)PlayerState.Flow] = ConfigManager.Instance.GetRoleData(2);
        this.stateDatas[(int)PlayerState.Power] = ConfigManager.Instance.GetRoleData(3);

        this.attackBoxSize = new Vector2(this.stateDatas[(int)this.state].eatLong, this.stateDatas[(int)this.state].eatWidth);
        this.attackBoxDir = new Vector2(0, 0);

        EventCenter.AddListener(EventType.OnLeftBtnPressed, MoveLeft);
        EventCenter.AddListener(EventType.OnRightBtnPressed, MoveRight);
        EventCenter.AddListener(EventType.OnJumpBtnClick, Jump);
        EventCenter.AddListener(EventType.OnKillBtnClick, Eat);
        //  EventCenter.AddListener(EventType.OnInterativeBtnClick, Interactive);
        EventCenter.AddListener(EventType.PlayerStopWalk, StopWalk);

        EventCenter.AddListener<int>(EventType.OnPowerStateBtnClick, ChangeState);
        EventCenter.AddListener<int>(EventType.OnFlowStateBtnClick, ChangeState);
        EventCenter.AddListener<int>(EventType.OnStickStateBtnClick, ChangeState);
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
            this.transform.position += new Vector3(-1 * this.stateDatas[(int)this.state].walkSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            this.transform.position += new Vector3(1 * this.stateDatas[(int)this.state].walkSpeed, 0, 0) * Time.deltaTime;
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

    public void ChangeState(int state)
    {
        //Debug.Log(state);
        for (int i = 0; i < 3; i++)
        {
            this.stateGos[i].SetActive(false);
        }
        this.stateGos[state].SetActive(true);
        this.state = (PlayerState)state;
    }

    private void StopWalk()
    {
        this.velocity.x = 0;
        this.stateAnims[(int)this.state].SetBool("walk", false);
    }
    private void MoveLeft()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        this.velocity.x = -1 * this.stateDatas[(int)this.state].walkSpeed;
        this.stateAnims[(int)this.state].SetBool("walk", true);

    }

    private void MoveRight()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        this.velocity.x = 1 * this.stateDatas[(int)this.state].walkSpeed;
        this.stateAnims[(int)this.state].SetBool("walk", true);
    }

    private void Jump()
    {
        if (this.grounded)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, this.stateDatas[(int)this.state].jumpForce));
            this.grounded = false;
        }

    }

    private void Eat()
    {
        this.stateAnims[(int)this.state].SetTrigger("eat");

        //  Debug.Log(this.transform.position);
        //  Debug.Log(this.transform.forward);
        Vector2 center = Vector2.zero;
        if (this.transform.rotation.y == 180)
        {
            center.x = this.transform.position.x + this.stateDatas[(int)this.state].eatLong;
        }
        else
        {
            center.x = this.transform.position.x - this.stateDatas[(int)this.state].eatWidth;
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

    //private void Interactive()
    //{
    //    Vector2 center = Vector2.zero;
    //    if (this.transform.rotation.y == 180)
    //    {
    //        center.x = this.transform.position.x + this.stateDatas[(int)this.state].eatLong;
    //    }
    //    else
    //    {
    //        center.x = this.transform.position.x - this.stateDatas[(int)this.state].eatWidth;
    //    }
    //    center.y = this.transform.position.y;

    //    //Debug.Log(center);

    //    RaycastHit2D hitInfo = Physics2D.BoxCast(center, this.attackBoxSize, 0, this.transform.forward, 10, LayerMask.GetMask("Interative"));

    //    if (hitInfo)
    //    {
    //        if (hitInfo.transform.CompareTag("Enemy"))
    //        {
    //            Debug.Log("interactive obj");
    //        }
    //    }
    //}

    public void Dead()
    {

    }


}
