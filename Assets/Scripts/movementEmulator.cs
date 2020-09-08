using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementEmulator : MonoBehaviour {

    public float moveSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward/moveSpeed;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward / moveSpeed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right / moveSpeed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right / moveSpeed;

        float pitch = Input.GetAxis("Mouse X");
        float yaw = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(-yaw*moveSpeed, pitch*moveSpeed, 0.0f);
    }
}
