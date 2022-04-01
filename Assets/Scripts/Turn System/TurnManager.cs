using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


/*
 * TurnManager
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Manages who's turn it is
*/
public class TurnManager : Singleton<TurnManager>
{
    public List<GameEvent> turnSubscribed = new List<GameEvent>();
    public GameEvent onFaseChangeEvent;
    public int currentTurn = -1;

    private Dictionary<GameEvent, int>  unsortedTurns = new Dictionary<GameEvent, int>();

    public void TurnSubscribe(GameEvent onTurn, int priority)
    {
        unsortedTurns.Add(onTurn, priority);
        ResetTurnManager();
    }

    public void nextTurn()
    {
        currentTurn++;
        if (currentTurn > turnSubscribed.Count)
        {
            currentTurn = 0;
        }
        if (currentTurn < turnSubscribed.Count)
        {
            turnSubscribed[currentTurn]?.Raise();
        } else
        {
            FaseManager.Instance.NextFase();
            currentTurn = 0;
            turnSubscribed[currentTurn]?.Raise();
        }
    }

    public void Unsubscribe(GameEvent theEvent)
    {
        unsortedTurns.Remove(theEvent);
        turnSubscribed.Remove(theEvent);
        if(currentTurn > turnSubscribed.Count)
        {
            currentTurn = 0;
        }
    }

    public void ResetTurnManager()
    {
        turnSubscribed = new List<GameEvent>();
        IOrderedEnumerable<KeyValuePair<GameEvent, int>> sorted = from entry in unsortedTurns orderby entry.Value ascending select entry;
        foreach (KeyValuePair<GameEvent, int> evI in sorted)
        {
            turnSubscribed.Add(evI.Key);
        }
        currentTurn = -1;
        nextTurn();
    }

    public void RemoveAllTurns()
    {
        unsortedTurns = new Dictionary<GameEvent, int>();
        turnSubscribed = new List<GameEvent>();
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetTurnManager();
    }
}
