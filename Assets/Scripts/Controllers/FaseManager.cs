using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
