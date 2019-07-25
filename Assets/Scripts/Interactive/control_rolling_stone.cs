using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_rolling_stone : BaseInteractive
{
    public GameObject door;
    Animator anim;
    public GameObject[] rolling_stone;
    bool is_begin, is_begin1, is_interactive;
    public float interval_time;
    float check_time;
    int i_3;
    Vector2 zero_velocity;
    bool is_close_door;

    Rigidbody2D[] rigid_of_stone = new Rigidbody2D[3];
    Vector3 start_position;
    Transform[] transform_of_stone = new Transform[3];
    int i;

    public override void Awake()
    {
        base.Awake();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
        base.Start();
        anim = door.GetComponent<Animator>();
        start_position = rolling_stone[0].transform.position;
        rolling_stone[1].SetActive(false);
        rolling_stone[2].SetActive(false);
        

        for (i = 0; i < 3; i ++)
        {
            rigid_of_stone[i] = rolling_stone[i].GetComponent<Rigidbody2D>();
            transform_of_stone[i] = rolling_stone[i].transform;
        }

        rigid_of_stone[0].gravityScale = 0;
        rigid_of_stone[1].gravityScale = 0;
        rigid_of_stone[2].gravityScale = 0;
        check_time = interval_time;
        is_begin = false;
        is_begin1 = false;
        is_interactive = true;
        i = 0;
        zero_velocity = new Vector2(0, 0);
        is_close_door = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_begin)
        {
            
            check_time -= Time.deltaTime;
            if (is_close_door && check_time < 4)
            {
                anim.SetFloat("Blend", -0.3f);
                is_close_door = false;
                rolling_stone[i + 1].SetActive(true);
            }
            if (check_time < 0)
            {
                anim.SetFloat("Blend", 0.3f);
                rigid_of_stone[i + 1].gravityScale = 1;
                check_time = interval_time;
                i += 1;
                is_close_door = true;
                if (i == 2)
                {
                    is_begin = false;
                    i = 0;
                    is_begin1 = true;
                }
            }
        }
        if (is_begin1)
        {
            check_time -= Time.deltaTime;
            if (is_close_door && check_time < 4)
            {
                i_3 = i % 3;
                anim.SetFloat("Blend", -0.3f);
                is_close_door = false;
                rigid_of_stone[i_3].gravityScale = 0;
                rigid_of_stone[i_3].velocity = zero_velocity;
                rigid_of_stone[i_3].angularVelocity = 0;
                transform_of_stone[i_3].position = start_position;
            }
            if (check_time < 0)
            {
                anim.SetFloat("Blend", 0.3f);
                rigid_of_stone[i_3].gravityScale = 1;
                check_time = interval_time;
                i += 1;
                is_close_door = true;
            }
        }
    }

    public override void InteractiveLogic(Transform player)
    {
        
        if (is_interactive)
        {
            this.KillTween();
            Debug.Log("player open the control door");
            anim.SetFloat("Blend", 0.3f);
            AudioManager.Instance.PlayClip("wall_break");
            rigid_of_stone[0].gravityScale = 1;
            AudioManager.Instance.PlayClip("stone_roll");
            is_begin = true;
            is_interactive = false;
        }
    }
}
