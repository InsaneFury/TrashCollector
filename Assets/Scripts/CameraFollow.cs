using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 distance;

    private void Start()
    {

    }

    private void Update()
    {
        transform.position = target.position + distance;
    }
}

