using Openverse.Events;
using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public Vector3Reference startingPosition;
    public bool isMyTurn = false;
    public float timeBetweenSteps = 0.1f;
    public float deadZone = 0.1f;
    public MeshRenderer mesh;

    [Header("Events")]
    public GameEvent playerTurnStartEvent;
    public GameEvent playerTurnEndEvent;

    private float walkCooldown = 0f;
    private float meshRotation = 0f;
    [HideInInspector]public Vector2Int currentPosition2D;

    public void GoToStartingPosition()
    {
        transform.position = startingPosition;
        currentPosition2D = new Vector2Int((int)startingPosition.Value.x, (int)startingPosition.Value.z);
        GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, true);
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        input = GameController.Instance.dollyParent.rotation * input;
        mesh.transform.localRotation = Quaternion.Slerp(mesh.transform.localRotation, Quaternion.Euler(Vector3.forward * meshRotation), 10f * Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position + input * 10);
        float Horizontal = input.x;
        float Vertical = input.z;
        switch(FaseManager.Instance.currentFase)
        {
            case GameFase.WALK:
                if (walkCooldown <= 0)
                {
                    GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, false);
                    if (Horizontal > deadZone)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x + 1, currentPosition2D.y))
                        {
                            currentPosition2D += new Vector2Int(1, 0);
                            GameController.Instance.roomGenerator.tiles[currentPosition2D.x, currentPosition2D.y].type.walkOnTileEvent?.Raise();
                            meshRotation = 90f;
                        }
                        walkCooldown = timeBetweenSteps;
                    }
                    if (Horizontal < -deadZone)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x - 1, currentPosition2D.y))
                        {
                            currentPosition2D += new Vector2Int(-1, 0);
                            GameController.Instance.roomGenerator.tiles[currentPosition2D.x, currentPosition2D.y].type.walkOnTileEvent?.Raise();
                            meshRotation = -90f;
                        }
                        walkCooldown = timeBetweenSteps;
                    }
                    if (Vertical > deadZone)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x, currentPosition2D.y + 1))
                        {
                            currentPosition2D += new Vector2Int(0, 1);
                            GameController.Instance.roomGenerator.tiles[currentPosition2D.x, currentPosition2D.y].type.walkOnTileEvent?.Raise();
                            meshRotation = 0;
                        }
                        walkCooldown = timeBetweenSteps;
                    }
                    if (Vertical < -deadZone)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x, currentPosition2D.y - 1))
                        {
                            currentPosition2D += new Vector2Int(0, -1);
                            GameController.Instance.roomGenerator.tiles[currentPosition2D.x, currentPosition2D.y].type.walkOnTileEvent?.Raise();
                            meshRotation = -180f;
                        }
                        walkCooldown = timeBetweenSteps;
                    }
                    GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, true);
                } else
                {
                    walkCooldown -= Time.deltaTime;
                }
                break;
            case GameFase.INTERACT:

                break;
            case GameFase.ATTACK:

                break;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentPosition2D.x, transform.position.y, currentPosition2D.y), 10f * Time.deltaTime);
    }

    public void onTurn()
    {
        isMyTurn = true;
        GameController.Instance.cameraTarget = transform;
        playerTurnStartEvent?.Raise();
    }

    public void FinishTurn()
    {
        isMyTurn = false;
        playerTurnEndEvent?.Raise();
        TurnManager.Instance.nextTurn();
    }
}
