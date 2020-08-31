using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public static MapMovement instance = null;
    private Event currentEvent;
    List<Event> availableEvents;
    [HideInInspector]
    public float roomMultiplier = 1f;
    public float roomMultiplierIncreasePerRoom = 0.08f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        InitializeManager();
    }

    public void InitializeManager()
    {
        roomMultiplier = 1f;
    }
    public void EventClicked(Event clickedEvent)
    {
        currentEvent = clickedEvent;
        SetNewAvaivableEvents(currentEvent.GetWays());
        roomMultiplier += roomMultiplierIncreasePerRoom;
        currentEvent.OnRoomEnter(roomMultiplier);
    }
    public void SetNewAvaivableEvents(List<Event> newAvaivableEvents)
    {
        if(availableEvents != null)
            foreach (Event oldEvent in availableEvents)
            {
                if(oldEvent)
                     oldEvent.SetAvailable(false);
            }

        availableEvents = newAvaivableEvents;

        foreach(Event newEvent in availableEvents)
        {
            newEvent.SetAvailable(true);
        }
        if (availableEvents.Count == 0)
            EventsSpawner.instance.LoadLevel(++EventsSpawner.instance.currentLevel);
    }
}
