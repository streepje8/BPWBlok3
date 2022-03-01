using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float distanceOfPlayer = 5f;
    public bool Smooth;

    void Update()
    {
        Vector3 goalPosition = new Vector3(player.position.x,distanceOfPlayer, player.position.z);
        transform.position = Smooth ? Vector3.Lerp(transform.position, goalPosition, 10f * Time.deltaTime) : goalPosition;
    }
}
