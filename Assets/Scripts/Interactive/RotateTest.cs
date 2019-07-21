using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateTest : MonoBehaviour
{
    public int interval = 2;
    private float timer = 1;
    public float angle = 100;
    private float dir = 1;

    private void Start()
    {
        transform.parent.Rotate(new Vector3(0, 0, angle/2));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ((int)timer % interval == 0)
        {
            dir = 1; this.transform.parent.Rotate(new Vector3(0, 0, 1), dir * angle * Time.deltaTime);
        }
        else
        {
            dir = -1; this.transform.parent.Rotate(new Vector3(0, 0, 1), dir * angle * Time.deltaTime);
        }
        
    }
}
