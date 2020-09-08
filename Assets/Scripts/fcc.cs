using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fcc : MonoBehaviour {

    static int atomNum = 14;
    private GameObject[] atoms = new GameObject[atomNum];
    private GameObject[] edges = new GameObject[12];
    public GameObject atomPrefab;
    public GameObject edgePrefab;
    //Atoms are arranged in [corners, centers]

    public float radius = 1, scale = 1, molScale = 1; //Radius is absolute, scale is ratio (atm:molecule)

    public float minScale = .1f;

    private Vector3 oldPos;

    // Use this for initialization
    void Start() {
        oldPos = transform.position;
        //Assuming center will be 0,0,0
        for (int i = 0; i < atomNum; ++i)
        {
            GameObject atm = Instantiate(atomPrefab);
            atm.transform.parent = transform;
            atm.transform.localScale = Vector3.one * (radius * 2) * scale;
            atm.GetComponent<Atom>().setParent(gameObject);
            atm.name = "Atom" + i;
            atm.tag = "Atom";
            atoms[i] = atm;
        }
        //Corners
        float a = 2 * Mathf.Sqrt(2) * radius;
        atoms[0].GetComponent<Atom>().setPosition(new Vector3(-a / 2, -a / 2, a / 2));
        atoms[1].GetComponent<Atom>().setPosition(new Vector3(a / 2, -a / 2, a / 2));
        atoms[2].GetComponent<Atom>().setPosition(new Vector3(-a / 2, a / 2, a / 2));
        atoms[3].GetComponent<Atom>().setPosition(new Vector3(a / 2, a / 2, a / 2));
        atoms[4].GetComponent<Atom>().setPosition(new Vector3(-a / 2, -a / 2, -a / 2));
        atoms[5].GetComponent<Atom>().setPosition(new Vector3(a / 2, -a / 2, -a / 2));
        atoms[6].GetComponent<Atom>().setPosition(new Vector3(-a / 2, a / 2, -a / 2));
        atoms[7].GetComponent<Atom>().setPosition(new Vector3(a / 2, a / 2, -a / 2));
        atoms[0].GetComponent<Renderer>().material.color = Color.red;
        atoms[1].GetComponent<Renderer>().material.color = Color.blue;
        atoms[2].GetComponent<Renderer>().material.color = Color.blue;
        atoms[3].GetComponent<Renderer>().material.color = Color.green;
        atoms[4].GetComponent<Renderer>().material.color = Color.blue;
        atoms[5].GetComponent<Renderer>().material.color = Color.green;
        atoms[6].GetComponent<Renderer>().material.color = Color.green;
        atoms[7].GetComponent<Renderer>().material.color = Color.red;
        //Face
        atoms[8].GetComponent<Atom>().setPosition(new Vector3(-a / 2, 0, 0));
        atoms[9].GetComponent<Atom>().setPosition(new Vector3(a / 2, 0, 0));
        atoms[10].GetComponent<Atom>().setPosition(new Vector3(0, -a / 2, 0));
        atoms[11].GetComponent<Atom>().setPosition(new Vector3(0, a / 2, 0));
        atoms[12].GetComponent<Atom>().setPosition(new Vector3(0, 0, -a / 2));
        atoms[13].GetComponent<Atom>().setPosition(new Vector3(0, 0, a / 2));
        atoms[8].GetComponent<Renderer>().material.color = Color.blue;
        atoms[9].GetComponent<Renderer>().material.color = Color.green;
        atoms[10].GetComponent<Renderer>().material.color = Color.blue;
        atoms[11].GetComponent<Renderer>().material.color = Color.green;
        atoms[12].GetComponent<Renderer>().material.color = Color.green;
        atoms[13].GetComponent<Renderer>().material.color = Color.blue;

        for(int i = 0; i < edges.Length; i++)
        {
            edges[i] = Instantiate(edgePrefab);
            edges[i].tag = "Edge";
            edges[i].transform.parent = transform;
        }

        //Lines/Edges
        setLinePos();

        foreach (GameObject edge in edges)
        {
            edge.GetComponent<LineRenderer>().startWidth = .005f;
            edge.GetComponent<LineRenderer>().endWidth = .005f;
            edge.GetComponent<LineRenderer>().startColor = Color.white;
            edge.GetComponent<LineRenderer>().endColor = Color.white;
        }
    }

    private void setLinePos()
    {
        edges[0].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[0].GetComponent<Atom>().getPosition(), atoms[1].GetComponent<Atom>().getPosition() });
        edges[1].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[1].GetComponent<Atom>().getPosition(), atoms[3].GetComponent<Atom>().getPosition() });
        edges[2].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[2].GetComponent<Atom>().getPosition(), atoms[3].GetComponent<Atom>().getPosition() });
        edges[3].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[2].GetComponent<Atom>().getPosition(), atoms[0].GetComponent<Atom>().getPosition() });
        edges[4].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[4].GetComponent<Atom>().getPosition(), atoms[5].GetComponent<Atom>().getPosition() });
        edges[5].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[5].GetComponent<Atom>().getPosition(), atoms[7].GetComponent<Atom>().getPosition() });
        edges[6].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[6].GetComponent<Atom>().getPosition(), atoms[7].GetComponent<Atom>().getPosition() });
        edges[7].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[6].GetComponent<Atom>().getPosition(), atoms[4].GetComponent<Atom>().getPosition() });
        edges[8].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[0].GetComponent<Atom>().getPosition(), atoms[4].GetComponent<Atom>().getPosition() });
        edges[9].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[1].GetComponent<Atom>().getPosition(), atoms[5].GetComponent<Atom>().getPosition() });
        edges[10].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[2].GetComponent<Atom>().getPosition(), atoms[6].GetComponent<Atom>().getPosition() });
        edges[11].GetComponent<LineRenderer>().SetPositions(new Vector3[] { atoms[3].GetComponent<Atom>().getPosition(), atoms[7].GetComponent<Atom>().getPosition() });
    }

    public void changeScale(float d)
    {
        scale += d;
        scale = Mathf.Clamp(scale, minScale, 1);
        foreach(Transform atom in transform.GetComponentsInChildren<Transform>())
        {
            if (atom.tag == "Atom" || atom.tag == "Ghost")
                atom.localScale = Vector3.one * (radius * 2) * scale / molScale;
        }
    }

    public void setScale(float s)
    {
        foreach (Transform atom in transform.GetComponentsInChildren<Transform>())
        {
            if (atom.tag == "Atom" || atom.tag == "Ghost")
                atom.localScale = Vector3.one * (radius * 2) * s / molScale;
        }
    }

    public void Hrotate(float hRot)
    {
        Vector3 rot = new Vector3(0,hRot, 0);
        transform.Rotate(rot, Space.Self);
        setLinePos();
    }

    public void Vrotate(float vRot)
    {
        Vector3 rot = new Vector3(vRot, 0, 0);
        transform.Rotate(rot, Space.Self);
        setLinePos();
    }

    private bool roomed = false;
    private float oldScale = 0;

    public void toggleRoom()
    {
        if (!roomed)
        {
            roomScale();
            roomed = true;
        }
        else
        {
            unroomScale();
            roomed = false;
        }
    }

    public void roomScale()
    {
        transform.position = Camera.main.transform.position;
        setScale(.1f);
        transform.localScale *= 10;
        molScale = 10;
        setLinePos();
        setScale(scale);
    }

    public void unroomScale()
    {
        transform.position = oldPos;
        setScale(scale);
        transform.localScale /= 10;
        molScale = 1;
        setLinePos();
        setScale(scale);
    }

    private bool lineOn = true;

    public void toggleLine()
    {
        lineOn = !lineOn;
        foreach (GameObject edge in edges)
        {
            edge.SetActive(lineOn);
        }
    }
}