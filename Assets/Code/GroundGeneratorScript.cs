using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneratorScript : GameplayObject {

    public GameObject GroundPrefab;

	// Use this for initialization
	void Start ()
    {
        HandleBirth();
	}
	
	// Update is called once per frame
	void Update ()
    {	
	}

    protected override void HandleBirth()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject g = Instantiate(GroundPrefab, this.gameObject.transform);
            g.transform.position = new Vector3(i, 0, 0);
        }
    }

    protected override void HandleMovement()
    {
        // The ground doesn't move for now.  Don't do anything.
    }

    protected override void HandleDeath()
    {
        // The ground doesn't die for now.  Don't do anything.
    }
}
