using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State { Alive, Dying, Transition }
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive){return;}

            switch (collision.gameObject.tag) { 

            case "Friendly":
                print ("Friendly!");
                break;
            case "Fuel":
                print ("Fuel");
                break;
            case "Finish":
                state = State.Transition;
                Invoke("EnterNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("EnterFirstLevel", 1f);
                break;
        }
    }

    private void EnterFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(1); // todo: Load more levels (n+1) 
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
