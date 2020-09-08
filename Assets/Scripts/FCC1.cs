using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCC1 : Molecule {

    int atomNum = 14;
    List<int[]> edgePair;

    void Start() {
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
        edgePair.Add(new int[] { 0, 1 });
    }

    public FCC1(int atomNum, List<int[]> edgePair):base(atomNum, edgePair)
    {

    }
}
