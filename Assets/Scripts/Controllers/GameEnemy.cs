using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemy : TileEntity
{
    public int tilesPerTurn = 10;
    public int moveToPlayerRadius = 4;
    public Vector2Int myPosition = Vector2Int.zero;
    public float timeBetweenMoves = 0.5f;
    public int staminaPerRound = 2;
    public float timeBetweenAttacks = 0.5f;
    public float DamagePerAttack = 4.5f;
    public float RegenFrom = 1f;
    public float RegenAmount = 0.5f;

    private bool myTurn = false;

    private float moveCooldown = 0f;
    private int tileBudget = 0;

    private float minTurnDuration = 0f;

    private Quaternion myRotation;
    private int stamina = 0;
    private float attackTimer = 0f;

    public void doTurn()
    {
        GameController.Instance.cameraTarget = transform;
        myTurn = true;
        minTurnDuration = 0.5f;
        tileBudget = tilesPerTurn;
    }

    public override void SpawnAt(int x, int y)
    {
        myPosition = new Vector2Int(x, y);
        GameController.Instance.roomGenerator.setContainsEntity(myPosition.x, myPosition.y, true);
    }

    private void Update()
    {
        if(myTurn)
        {
            if(moveCooldown > 0f)
            {
                moveCooldown -= Time.deltaTime;
            }
            switch (FaseManager.Instance.currentFase)
            {
                case GameFase.WALK:
                    if (Vector2Int.Distance(myPosition, GameController.Instance.player.currentPosition2D) < 2f)
                    {
                        tileBudget = 0;
                    }
                    if (tileBudget > 0)
                    {
                        if (moveCooldown <= 0f)
                        {
                            Vector2Int desiredMovement = Vector2Int.zero;
                            if (Vector2Int.Distance(myPosition, GameController.Instance.player.currentPosition2D) < moveToPlayerRadius)
                            {
                                Vector2Int direction = (GameController.Instance.player.currentPosition2D - myPosition);
                                direction = new Vector2Int(Mathf.RoundToInt(direction.x / direction.magnitude), Mathf.RoundToInt(direction.y / direction.magnitude));
                                desiredMovement = direction;
                            }
                            else
                            {
                                desiredMovement = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2)); //Wander around
                            }
                            GameController.Instance.roomGenerator.setContainsEntity(myPosition.x, myPosition.y, false);
                            if (GameController.Instance.roomGenerator.isWalkableAndFree(myPosition.x + desiredMovement.x, myPosition.y + desiredMovement.y))
                            {
                                myRotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(desiredMovement.x,0, desiredMovement.y));
                                myPosition += desiredMovement;
                            }
                            GameController.Instance.roomGenerator.setContainsEntity(myPosition.x, myPosition.y, true);
                            tileBudget--;
                            moveCooldown = timeBetweenMoves;
                        }
                    }
                    else
                    {
                        minTurnDuration -= Time.deltaTime;
                        if (minTurnDuration <= 0f)
                        {
                            stamina = staminaPerRound;
                            myTurn = false;
                            TurnManager.Instance.nextTurn();
                        }
                    }
                    break;
                case GameFase.INTERACT:
                    minTurnDuration -= Time.deltaTime;
                    if (minTurnDuration <= 0f)
                    {
                        myTurn = false;
                        TurnManager.Instance.nextTurn();
                    }
                    break;
                case GameFase.ATTACK:
                    if (stamina > 0 && Vector2Int.Distance(myPosition, GameController.Instance.player.currentPosition2D) < 2)
                    {
                        if(attackTimer <= 0f)
                        {
                            if (HP > RegenFrom)
                            {
                                PlayerStats.Instance.DamagePlayer(DamagePerAttack);
                                transform.rotation *= Quaternion.Euler(Vector3.up * 25f); //animation :>
                                attackTimer = timeBetweenAttacks;
                            } else
                            {
                                HP += RegenAmount;
                                attackTimer = timeBetweenAttacks;
                            }
                            stamina--;
                        } else
                        {
                            attackTimer -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        minTurnDuration -= Time.deltaTime;
                        if (minTurnDuration <= 0f)
                        {
                            myTurn = false;
                            TurnManager.Instance.nextTurn();
                        }
                    }
                    break;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, myRotation, 10f * Time.deltaTime);
            Vector3 forward = transform.forward;
            forward.y = 0;
            transform.forward = forward.normalized;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(myPosition.x, transform.position.y, myPosition.y), 10f * Time.deltaTime);
        //transform.position = new Vector3(myPosition.x, transform.position.y, myPosition.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, moveToPlayerRadius);
    }
}
