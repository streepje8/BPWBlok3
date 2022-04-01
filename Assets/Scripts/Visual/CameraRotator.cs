using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}
