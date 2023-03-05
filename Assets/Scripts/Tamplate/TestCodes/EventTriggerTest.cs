using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Utils;

public class EventTriggerTest : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string test = "test";
            EventManager.TriggerEvent(Events.TEST_EVENT, true, new object[] { test });
        }
    }
}
