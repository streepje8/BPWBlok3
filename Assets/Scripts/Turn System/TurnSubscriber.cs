using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
}
