using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Observer Object that sits in memory
[CreateAssetMenu(menuName = "Signal Sender")]
public class SignalSender : ScriptableObject
{
    public List<SignalListener> listenerList = new List<SignalListener>(); // list of signal listeners listening to this sender

    public void Raise() // raise a signal
    {
        for (int i = listenerList.Count - 1; i >= 0; i--) // go through the list of listeners backwords
        {
            listenerList[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener) // add listener to list
    {
        listenerList.Add(listener);
    }

    public void DeregisterListener(SignalListener listener) // remove listener from list
    {
        listenerList.Remove(listener);
    }
}
