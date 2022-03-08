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

    [Header("Events")]
    public GameEvent playerTurnStartEvent;
    public GameEvent playerTurnEndEvent;

    private bool walkReleased = true;
    [HideInInspector]public Vector2Int currentPosition2D;

    public void GoToStartingPosition()
    {
        transform.position = startingPosition;
        currentPosition2D = new Vector2Int((int)startingPosition.Value.x, (int)startingPosition.Value.z);
        GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, true);
    }

    private void Update()
    {
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) < 0.001f) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.001f))
        {
            walkReleased = true;
        }
        switch(FaseManager.Instance.currentFase)
        {
            case GameFase.WALK:
                if (walkReleased)
                {
                    GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, false);
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x + 1, currentPosition2D.y))
                        {
                            currentPosition2D += new Vector2Int(1, 0);
                        }
                        walkReleased = false;
                    }
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x - 1, currentPosition2D.y))
                        {
                            currentPosition2D += new Vector2Int(-1, 0);
                        }
                        walkReleased = false;
                    }
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x, currentPosition2D.y + 1))
                        {
                            currentPosition2D += new Vector2Int(0, 1);
                        }
                        walkReleased = false;
                    }
                    if (Input.GetAxis("Vertical") < 0)
                    {
                        if (GameController.Instance.roomGenerator.isWalkableAndFree(currentPosition2D.x, currentPosition2D.y - 1))
                        {
                            currentPosition2D += new Vector2Int(0, -1);
                        }
                        walkReleased = false;
                    }
                    GameController.Instance.roomGenerator.setContainsEntity(currentPosition2D.x, currentPosition2D.y, true);
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
