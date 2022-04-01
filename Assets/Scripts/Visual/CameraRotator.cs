using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CameraRotator
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A Script that rotates the camera (well technically any game object)
 */
public class CameraRotator : MonoBehaviour
{
    public Vector3 axis;
    public float speed;
    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}
