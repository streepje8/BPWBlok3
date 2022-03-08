using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float distanceOfPlayer = 10f;
    public bool Smooth;

    void Update()
    {
        Vector3 goalPosition = new Vector3(target.position.x,distanceOfPlayer, target.position.z);
        transform.position = Smooth ? Vector3.Lerp(transform.position, goalPosition, 10f * Time.deltaTime) : goalPosition;
    }
}
