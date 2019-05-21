﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
    }

     void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print ("Friendly!");
                break;
            case "Fuel":
                print ("Fuel");
                break;
            case "Finish":
                print("you łin");
                SceneManager.LoadScene(1);
                break;
            default:
                print("you Ded");
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            //  print ("turning left");
            
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // print("turning right");
            transform.Rotate(Vector3.back * rotateThisFrame);
        }

        rigidBody.freezeRotation = false;
    }
}
