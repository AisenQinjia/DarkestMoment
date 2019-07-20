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

        EventCenter.AddListener<GameObject>(EventType.OnClickInteractive, Interactive);
    }

    void Start()
    {
        this.ChangeState((int)this.state);

    }
    public override void OnDestroy()
    {
        base.OnDestroy();

        EventCenter.RemoveListener(EventType.OnLeftBtnPressed, MoveLeft);
        EventCenter.RemoveListener(EventType.OnRightBtnPressed, MoveRight);
        EventCenter.RemoveListener(EventType.OnJumpBtnClick, Jump);
        EventCenter.RemoveListener(EventType.OnKillBtnClick, Eat);
        //  EventCenter.AddListener(EventType.OnInterativeBtnClick, Interactive);
        EventCenter.RemoveListener(EventType.PlayerStopWalk, StopWalk);

        EventCenter.RemoveListener<int>(EventType.OnPowerStateBtnClick, ChangeState);
        EventCenter.RemoveListener<int>(EventType.OnFlowStateBtnClick, ChangeState);
        EventCenter.RemoveListener<int>(EventType.OnStickStateBtnClick, ChangeState);

        EventCenter.RemoveListener<GameObject>(EventType.OnClickInteractive, Interactive);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

            UIManager.Instance.PopHint("你好棒！");
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            this.transform.position += new Vector3(-1 * this.stateDatas[(int)this.state].walkSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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
        if (this.stateAnims[(int)this.state] != null)
            this.stateAnims[(int)this.state].SetBool("walk", false);
    }
    private void MoveLeft()
    {

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        this.velocity.x = -1 * this.stateDatas[(int)this.state].walkSpeed;
        if (this.stateAnims[(int)this.state] != null)
            this.stateAnims[(int)this.state].SetBool("walk", true);

    }

    private void MoveRight()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));


        this.velocity.x = 1 * this.stateDatas[(int)this.state].walkSpeed;
        if (this.stateAnims[(int)this.state] != null)
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
        if (this.stateAnims[(int)this.state] != null)
            this.stateAnims[(int)this.state].SetTrigger("eat");

        Debug.Log(this.transform.position);

        Vector2 origin = Vector2.zero;

        //center.x = this.transform.position.x + this.transform.right.x * this.stateDatas[(int)this.state].eatLong;

        //center.y = this.transform.position.y;
        origin.x = this.transform.position.x;
        origin.y = this.transform.position.y;

        Debug.Log(origin);
        Debug.Log(this.attackBoxSize);


        RaycastHit2D hitInfo = Physics2D.BoxCast(origin, this.attackBoxSize, 0, new Vector2(this.transform.right.x, this.transform.position.y), this.stateDatas[(int)this.state].eatLong, LayerMask.GetMask("Enemy"));
        // public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask);

        if (hitInfo)
        {
            // if (hitInfo.transform.CompareTag("Enemy"))
            // {
            Debug.Log(hitInfo.transform.name);
            BaseRoleController ctrl = hitInfo.transform.GetComponent<BaseRoleController>();
            if (ctrl == null)
            {
                ctrl = hitInfo.transform.GetComponentInParent<BaseRoleController>();
                ctrl.OnDead();
            }
            ctrl.OnDead();
            // }
        }
    }

    private void OnDrawGizmos()
    {
        if (this.transform == null || this.stateDatas[(int)this.state] == null)
            return;
        Gizmos.color = Color.yellow;
        Vector2 center = Vector2.zero;

        center.x = this.transform.position.x + this.transform.right.x * this.stateDatas[(int)this.state].eatLong;

        center.y = this.transform.position.y;

        Gizmos.DrawCube(center, this.attackBoxSize);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, this.stateDatas[(int)this.state].interativeRange);
    }


    private void Interactive(GameObject go)
    {
        if (Vector3.Distance(go.transform.position, this.transform.position) <= this.stateDatas[(int)this.state].interativeRange)
        {
            go.GetComponent<BaseInteractive>().InteractiveLogic(go.transform);
        }
        else
        {
            if (this.state.Equals(PlayerState.Power))
                UIManager.Instance.PopHint("你的念力太差了，差一点点！");
            else
                UIManager.Instance.PopHint("靠近一点才能点我哦~,除非你是念力大师！");
        }
    }

    public override void OnDead()
    {
        this.enabled = false;
        EventCenter.Broadcast(EventType.OnPlayerDead);
    }


}
