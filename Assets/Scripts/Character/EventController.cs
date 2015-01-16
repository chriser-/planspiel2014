using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class EventController : MonoBehaviour {

    public delegate void ResetLevel();
    public static event ResetLevel OnReset;

    public void Trigger(string eventName, object[] arguments = null)
    {
        switch (eventName)
        {
            case "OnReset": OnReset(); break;
        }
    }
}
