using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public bool hasOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

    private void Start()
    {
        if (!hasOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }
}
