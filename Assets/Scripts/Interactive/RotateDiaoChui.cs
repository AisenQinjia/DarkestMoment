using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDiaoChui : MonoBehaviour
{
    public float speed = 1;
    private float timer ;
    float time;
    private int coeffient;
    public float angle = 100;
    private float dir = -1;

    void Start()
    {
        transform.parent.Rotate(new Vector3(0, 0, angle * 0.5f));
        coeffient = 1;
        time = 1 / speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > coeffient* time)
        {
            dir = -dir;
            coeffient++;
        }
        this.transform.parent.Rotate(new Vector3(0, 0, 1), speed * dir * angle * Time.deltaTime);
    }
}
