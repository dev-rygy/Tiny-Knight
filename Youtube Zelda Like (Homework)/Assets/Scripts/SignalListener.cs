using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Using Unity's Event System

public class SignalListener : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent;
    
    public void OnSignalRaised()
    {
        signalEvent.Invoke(); // Call the event
    }

    private void OnEnable() // Create a listener
    {
        signal.RegisterListener(this);
    }

    private void OnDisable() // Destroy a listener
    {
        signal.DeregisterListener(this);
    }
}
