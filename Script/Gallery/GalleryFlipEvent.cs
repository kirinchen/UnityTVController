using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GalleryFlipEvent : MonoBehaviour {

    private Gallery gallery = null;

    public bool autoInject = false;

    void Start() {
        if (autoInject) {
            injectEvets(gameObject);
        }
    }

    private Gallery getGallery() {
        if (gallery == null) {
            gallery = GetComponentInParent<Gallery>();
        }
        return  gallery;
    }

    private void injectEvets(GameObject go) {
        EventTrigger eventTrigger = go.AddComponent<EventTrigger>();
        addEventTrigger(eventTrigger, onMouseDown, EventTriggerType.PointerDown);
        addEventTrigger(eventTrigger, onMouseUp, EventTriggerType.PointerUp);
        addEventTrigger(eventTrigger, onMouseMove, EventTriggerType.Drag);
    }

    private void addEventTrigger(EventTrigger eventTrigger, UnityAction action, EventTriggerType triggerType) {
        // Create a nee TriggerEvent and add a listener
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener((eventData) => action()); // you can capture and pass the event data to the listener

        // Create and initialise EventTrigger.Entry using the created TriggerEvent
        EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

        // Add the EventTrigger.Entry to delegates list on the EventTrigger
        eventTrigger.triggers.Add(entry);
    }

    public void onMouseDown() {
        getGallery().onMouseDown();
    }

    public void onMouseMove() {
        getGallery().onMouseMove();
    }

    public void onMouseUp() {
        getGallery().onMouseUp();
    }


}
