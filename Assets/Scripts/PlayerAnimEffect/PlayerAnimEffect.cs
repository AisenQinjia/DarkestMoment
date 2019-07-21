using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimEffect : MonoBehaviour
{

    void EatEffect()
    {
        EventCenter.Broadcast(EventType.CameraShake);
    }
}
