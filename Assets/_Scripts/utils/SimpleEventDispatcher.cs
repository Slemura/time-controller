using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEventDispatcher : MonoBehaviour {
    
    class Event {
        public Action action;
        public int hash;
    }    

    private Dictionary<string, List<Event>> listeners;

    void Awake() {
        listeners = new Dictionary<string, List<Event>>();
    }

    public virtual void AddListener(string eve, Action listener) {
        if (!listeners.ContainsKey(eve)) {
            listeners.Add(eve, new List<Event>());
        }
        
        if(FindExistEvent(eve, listener.GetHashCode()) == null) {
            Event evt = new Event();
            evt.action = listener;
            evt.hash = listener.GetHashCode();
            listeners[eve].Add(evt);
        }
    }

    public virtual void RemoveListener(string eve, Action listener) {
        Event deleted_event = FindExistEvent(eve, listener.GetHashCode());
        if(deleted_event != null) {
            listeners[eve].Remove(deleted_event);
            deleted_event = null;
        }
    }

    public virtual void DispatchEvent(string eve) {
        if (listeners.ContainsKey(eve)) {
            for (int i = 0; i < listeners[eve].Count; i++) {
                listeners[eve][i].action();
            }
            /*foreach (Event evt in listeners[eve]) {
                evt.action();
            }*/
        }
    }

    private Event FindExistEvent(string eve, int hash) {
        if (listeners.ContainsKey(eve)) {
            foreach (Event evt in listeners[eve]) {
                if(evt.hash == hash) {
                    return evt;
                }
            }
        }
        return null;
    }
}
