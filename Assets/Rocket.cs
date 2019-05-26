using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 50f;
    [SerializeField] float mainThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finishedLevel;

    [SerializeField] ParticleSystem ThrusterSmoke;
    [SerializeField] ParticleSystem ParticleExplosion;
    [SerializeField] ParticleSystem ParticleFinish;
    int levelnumber;
    
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
            RespondToThrustInput();
            RespondToRotateInput();
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
                StartWinSequence();
                break;
            default:
                StartDyingSequence();
                break;
        }
    }

    private void StartDyingSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        Invoke("EnterFirstLevel", 3f);
        ParticleExplosion.Play();
    }

    private void StartWinSequence()
    {
        state = State.Transition;
        audioSource.Stop();
        audioSource.PlayOneShot(finishedLevel);
        Invoke("EnterNextLevel", 3f);
        ParticleFinish.Play();
    }

    private void EnterFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(1); // todo: Load more levels (n+1) 
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ThrustForward();
        }
        else
        {
            audioSource.Stop();
            ThrusterSmoke.Stop();
        }
    }

    private void ThrustForward()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        ThrusterSmoke.Play();
    }

    private void RespondToRotateInput()
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
