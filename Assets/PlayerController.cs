using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3Reference startingPosition;
    public void GoToStartingPosition()
    {
        transform.position = startingPosition;
    }
}
