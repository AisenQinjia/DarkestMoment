using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle10s : BaseInteractive
{
    float burning_time = 10f;
    Animator anim;
    bool is_begin = false;
    GameObject[] control_enemy;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = GameDefine.InterativeTag;
    }

    void Update()
    {
        if (is_begin)
        {
            Debug.Log("go");
            burning_time -= Time.deltaTime;
            if (burning_time <= 0)
            {
                Debug.Log("destroy candle");
                
                foreach (GameObject i in control_enemy)
                {
                    i.GetComponent<BaseRoleController>().LeaveMe();
                    Destroy(gameObject);
                }
            }
        }
    }

    public override void InteractiveLogic(Transform player)
    {
        
        Debug.Log("candle attract enemy");
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0.3f);
        
        foreach (GameObject i in control_enemy)
        {
            i.GetComponent<BaseRoleController>().ComeToMe(transform);
        }
        is_begin = true;
        Debug.Log("candle attract enemy end");
    }
}
