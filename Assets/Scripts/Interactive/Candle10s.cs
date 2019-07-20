using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle10s : BaseInteractive
{
    float burning_time = 10f;
    Animator anim;
    bool is_begin = false;
    BaseRoleController[] control_enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (is_begin)
        {
            burning_time -= Time.deltaTime;
            if (burning_time <= 0)
            {
                foreach (BaseRoleController i in control_enemy)
                {
                    i.LeaveMe();
                }
            }
        }
    }

    public override void InteractiveLogic(Transform player)
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0.3f);
        //if (control_enemy.Length == 0)
        //    return;
        foreach (BaseRoleController i in control_enemy)
        {
            i.ComeToMe(transform);
        }
        is_begin = true;
    }
}
