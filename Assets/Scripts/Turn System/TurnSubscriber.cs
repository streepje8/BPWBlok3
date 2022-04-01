using Openverse.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * TurnSubscriber
 * Wessel Roelofse
 * 01/04/2022
 * 
 * Subscribes a function of this gameobject to the TurnManager
 */
public class TurnSubscriber : GameEventListener
{
    public int priority = 100;

    private new void Awake()
    {
        if(Event != null)
        {
            Debug.LogError("The Event Field Of A TurnSubscriber Gets Assigned Automatically. Please Do Not Assign Anything In The Field.");
        }
        Event = ScriptableObject.CreateInstance<GameEvent>();
        TurnManager.Instance.TurnSubscribe(Event, priority);
        base.Awake();
    }

    public void ReSubscribe()
    {
        if (Event != null)
        {
            Event = ScriptableObject.CreateInstance<GameEvent>();
        }
        TurnManager.Instance.TurnSubscribe(Event, priority);
        base.Awake();
    }
}
