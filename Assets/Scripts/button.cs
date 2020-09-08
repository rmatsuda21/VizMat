using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class button : MonoBehaviour {

    public UnityEvent clickEvent;

    public void onClick()
    {
        clickEvent.Invoke();
    }

    public void setGaze(bool gaze)
    {
        GetComponent<Renderer>().material.color = gaze ? Color.blue : Color.white;
    }
}
