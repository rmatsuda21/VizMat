using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    bool lookingAtAtom = false;
    bool lookingAtButton = false;
    bool isHolding = false;
    GameObject holdingAtm = null;

    bool doubleTap = false;

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.forward*5, Color.red);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5.0f))
        {
            if (hit.collider.gameObject.tag == "Atom")
            {
                lookingAtAtom = true;
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
                lookingAtAtom = false;
            if (hit.collider.gameObject.tag == "Button")
            {
                lookingAtButton = true;
            }
            else
                lookingAtButton = false;
        }
        else
        {
            lookingAtAtom = false;
            lookingAtButton = false;
        }

        if (Input.GetButtonDown("Fire1") && isHolding)
        {
            isHolding = false;
            holdingAtm.GetComponent<Atom>().detachAtom();
            if (doubleTap)
                holdingAtm.GetComponent<Atom>().resetPos();
            holdingAtm = null;

        }
        else if (Input.GetButtonDown("Fire1") && lookingAtButton)
        {
            hit.collider.gameObject.GetComponent<button>().onClick();
        }
        else if (Input.GetButtonDown("Fire1") && lookingAtAtom && !isHolding)
        {
            holdingAtm = hit.collider.gameObject;
            if(holdingAtm.tag == "Atom"){
                isHolding = true;
                holdingAtm.GetComponent<Atom>().attachAtom();

                StartCoroutine(doubleTapTimer());
            }
        }


	}

    IEnumerator doubleTapTimer()
    {
        doubleTap = true;
        yield return new WaitForSecondsRealtime(0.25f);
        doubleTap = false;
    }
}
