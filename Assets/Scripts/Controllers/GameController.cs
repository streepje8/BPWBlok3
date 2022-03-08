using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public RoomGenerator roomGenerator;
    public PlayerController player;
    public Transform cameraTarget;
    public FollowTarget followTarget;
    public Transform dollyParent;

    private void Update()
    {
        followTarget.target = cameraTarget;
    }

    private void Awake()
    {
        Instance = this;   
    }
}
