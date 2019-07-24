using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownWood : MonoBehaviour
{
    public float move_length;
    public float move_velocity;
    float left_point;
    float right_point;
    float x_move;

    // Start is called before the first frame update
    void Start()
    {
        left_point = transform.position.y;
        right_point = left_point + move_length;
        x_move = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + x_move * Time.deltaTime * move_velocity, transform.position.z);
        if (transform.position[1] > right_point)
        {
            x_move = -1;
        }
        if (transform.position[1] < left_point)
        {
            x_move = 1;
        }
    }
}
