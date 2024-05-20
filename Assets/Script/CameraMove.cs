using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // public void GameStart()
    // {
    //     //target = GameObject.FindGameObjectWithTag("Player");
    // }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smootedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smootedPosition;
        }
    }
}
