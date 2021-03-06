﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3 (10f, 10f, 10f);
    [SerializeField] float period = 2f;

    [Range(0, 1)]
    [SerializeField]
    float movementFactor;
    Vector3 startingPos;


	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (period != 0)
        {
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);
            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementFactor * movementVector;
            transform.position = startingPos + offset;
        }
        else
        { 
            print("Error code 1: There's division by zero");
        }
    }
}
