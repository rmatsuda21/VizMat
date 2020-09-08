using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bcc : MonoBehaviour
{

    static int atomNum = 9;
    private GameObject[] atoms = new GameObject[atomNum];
    public GameObject atomPrefab;
    //Atoms are arranged in [corners, centers]

    public float radius = 1, scale = 1; //Radius is absolute, scale is ratio

    // Use this for initialization
    void Start()
    {
        //Assuming center will be 0,0,0
        for (int i = 0; i < atomNum; i++)
        {
            atoms[i] = Instantiate(atomPrefab);
            //atoms[i].AddComponent<Atom>();
            atoms[i].transform.parent = transform;
            atoms[i].transform.localScale = Vector3.one * (radius * 2) * scale;
            atoms[i].tag = "Atom";
        }
        //Corners
        float a = 4/Mathf.Sqrt(3) * radius;
        atoms[0].GetComponent<Atom>().setPosition(new Vector3(-a / 2, -a / 2, a / 2));
        atoms[1].GetComponent<Atom>().setPosition(new Vector3(a / 2, -a / 2, a / 2));
        atoms[2].GetComponent<Atom>().setPosition(new Vector3(-a / 2, a / 2, a / 2));
        atoms[3].GetComponent<Atom>().setPosition(new Vector3(a / 2, a / 2, a / 2));
        atoms[4].GetComponent<Atom>().setPosition(new Vector3(-a / 2, -a / 2, -a / 2));
        atoms[5].GetComponent<Atom>().setPosition(new Vector3(a / 2, -a / 2, -a / 2));
        atoms[6].GetComponent<Atom>().setPosition(new Vector3(-a / 2, a / 2, -a / 2));
        atoms[7].GetComponent<Atom>().setPosition(new Vector3(a / 2, a / 2, -a / 2));
        for (int i = 0; i < 8; i++)
            atoms[i].GetComponent<Renderer>().material.color = Color.red;
        //Face
        atoms[8].GetComponent<Atom>().setPosition(Vector3.zero);
        atoms[8].GetComponent<Renderer>().material.color = Color.green;

        //transform.Rotate(new Vector3(45, 45, 45), Space.World);
    }

    public void changeScale(float d)
    {
        scale += d;
        scale = Mathf.Clamp(scale, 0, 1);
        foreach (Transform atom in transform.GetComponentsInChildren<Transform>())
        {
            if (atom.tag == "Atom")
                atom.localScale = Vector3.one * (radius * 2) * scale;
        }
    }

    public void Hrotate(float hRot)
    {
        Vector3 rot = new Vector3(0, hRot, 0);
        transform.Rotate(rot, Space.Self);
    }

    public void Vrotate(float vRot)
    {
        Vector3 rot = new Vector3(vRot, 0, 0);
        transform.Rotate(rot, Space.Self);
    }
}