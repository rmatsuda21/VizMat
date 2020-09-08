using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Atom : MonoBehaviour {

    private GameObject parent;

    public Vector3 initPos;
    public Vector3 initScale;
    public double scale;

    public bool ghosting = false;

    private float snapDist = 0.1f;

    private Material ghostMat;
    private GameObject ghostObj;
    // Use this for initialization
    void Start () {
        ghostMat = Resources.Load<Material>("Ghost");
        initScale = transform.localScale;
        gameObject.AddComponent<EventTrigger>();
    }

    public void setParent(GameObject p)
    {
        parent = p;
    }

    public void setPosition(Vector3 pos)
    {
        gameObject.transform.localPosition = pos;
        initPos = gameObject.transform.localPosition;
    }

    public Vector3 getPosition()
    {
        if (!ghosting)
            return transform.position;
        else
            return ghostObj.transform.position;
    }

    public bool attached = false;

    public void toggleAttach()
    {
        if (!attached)
        {
            attachAtom();
            attached = true;
        }
        else
        {
            detachAtom();
            attached = false;
        }
    }

    public void attachAtom()
    {
        gameObject.transform.parent = Camera.main.transform;
        if (!ghosting)
        {
            ghostObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ghostObj.name = "Ghost";
            ghostObj.tag = "Ghost";
            ghostObj.GetComponent<Renderer>().material = ghostMat;
            ghostObj.transform.parent = GameObject.Find("FCC").transform;
            ghostObj.transform.localPosition = initPos;

            float scale = ghostObj.transform.parent.GetComponent<fcc>().scale;

            ghostObj.transform.localScale = initScale*scale/parent.GetComponent<fcc>().molScale;
            ghosting = true;
        }
    }

    private bool canSnap(Vector3 pos)
    {
        return Vector3.Magnitude(pos - ghostObj.transform.position) < snapDist;
    }

    public void resetPos()
    {
        Destroy(ghostObj);
        ghosting = false;
        gameObject.transform.localPosition = initPos;
    }

    public void detachAtom()
    {
        gameObject.transform.parent = GameObject.Find("FCC").transform;
        if (canSnap(gameObject.transform.position))
        {
            resetPos();
        }
    }

    private Color temp;

    public void setGaze(bool gaze)
    {
        if(gaze)
            temp = GetComponent<Renderer>().material.color;
        Color gazeColor = temp;
        gazeColor.a = 0.2f;
        GetComponent<Renderer>().material.color = gaze ? gazeColor : temp;
    }
}
