using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//玩家动画帧事件动效控制脚本
public class PlayerAnimEffect : MonoBehaviour
{

    private PlayerController player;

    private void Start()
    {
        player = this.GetComponentInParent<PlayerController>();
    }

    public void EatEffect()
    {
        player.EatJudge();
        EventCenter.Broadcast(EventType.CameraShake);
    }
}
