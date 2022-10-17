using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
            }
            else
            {
                eventManager.Init();

                DontDestroyOnLoad(eventManager);
            }
            return eventManager;
        }

    }

    void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listner)
    {
        if (eventManager == null) return;

        Action<Dictionary<string, object>> thisEvent;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listner;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
    }

}
