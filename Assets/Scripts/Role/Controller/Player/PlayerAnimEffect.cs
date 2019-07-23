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
        if (player.State == (int)PlayerState.Power)
        {
            return;
        }

        if (player.EatJudge())
        {
            EventCenter.Broadcast(EventType.CameraHightLight);
            EventCenter.Broadcast<float, float>(EventType.CameraShake, 0.2f, 2f);

        }
        else
        {
            EventCenter.Broadcast<float, float>(EventType.CameraShake, 0.05f, 0.5f);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClip(GameDefine.playerEat);

    }
}
