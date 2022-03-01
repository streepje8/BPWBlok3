using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnSubscriber : GameEventListener
{

    private void Awake()
    {
        Event = new GameEvent();
        TurnManager.Instance.TurnSubscribe(Event);
    }
}
