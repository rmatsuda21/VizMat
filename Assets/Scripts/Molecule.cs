using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour {

    int atomNum;
    private List<GameObject> atoms;
    private List<GameObject> edges;
    private List<int[]> edgePair;

    public GameObject atomPrefab;
    public GameObject edgePrefab;

    public float radius, scale, molScale;

    public Molecule(int atomNum, List<int[]> edgePair) {
        radius = 1;
        scale = 1;
        molScale = 1;

        atoms = new List<GameObject>();
        edges = new List<GameObject>();
        this.edgePair.AddRange(edgePair);

        for(int i = 0; i < atomNum; ++i) {
            GameObject atm = Instantiate(atomPrefab);
            atm.transform.parent = transform;
            atm.transform.localScale = Vector3.one * (radius * 2) * scale;
            atm.GetComponent<Atom>().setParent(gameObject);
            atm.name = "Atom" + i;
            atm.tag = "Atom";
            atoms.Add(atm);
        }
    }

    void Start() { 

    }

}
