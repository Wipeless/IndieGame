using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up, 20);
        //transform.localRotation = Quaternion.Euler(new Vector3(0, Time.time * 300, 0));
	}
}
