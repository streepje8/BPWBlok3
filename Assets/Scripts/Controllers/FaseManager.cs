using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * FaseManager
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A script that manages what fase the game currently is in.
 */
public enum GameFase
{
    WALK,
    INTERACT,
    ATTACK
}

public class FaseManager : Singleton<FaseManager>
{

    public GameFase currentFase = GameFase.WALK; 

    private void Awake()
    {
        Instance = this;
    }

    public void NextFase()
    {
        currentFase++;
        if(currentFase > GameFase.ATTACK)
        {
            currentFase = GameFase.WALK;
        }
    }
}
