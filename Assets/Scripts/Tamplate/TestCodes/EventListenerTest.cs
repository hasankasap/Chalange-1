using Game;
using Game.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerTest : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening(Events.TEST_EVENT, listenerTest);
    }
    private void OnDisable()
    {
        EventManager.StopListening(Events.TEST_EVENT, listenerTest);
    }
    private void listenerTest(object[] obj)
    {
        string data = (string)obj[0];

        Debug.Log(data);
    }
}
