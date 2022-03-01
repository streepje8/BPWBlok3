using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    public List<GameEvent> turnSubscribed = new List<GameEvent>();
    public int currentTurn = -1;

    public void TurnSubscribe(GameEvent onTurn)
    {
        turnSubscribed.Add(onTurn);
    }

    public void nextTurn()
    {
        currentTurn++;
        if(currentTurn < turnSubscribed.Count)
        {
            turnSubscribed[currentTurn]?.Raise();
        } else
        {
            currentTurn = 0;
            turnSubscribed[currentTurn]?.Raise();
        }
    }

    void Awake()
    {
        Instance = this;
    }    
}
