using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {


    Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("space");
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
//  print ("turning left");
            transform.Rotate(Vector3.forward*Time.deltaTime*50);
        }
        else if (Input.GetKey(KeyCode.D))
            {
            // print("turning right");
            transform.Rotate(Vector3.back * Time.deltaTime*50);
        }
    }
}
