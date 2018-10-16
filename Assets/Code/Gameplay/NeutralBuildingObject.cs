using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralBuildingObject : GameplayObject {

    public int m_length = 10;

    private Vector3 m_startingPoint;
    private Vector3 m_endPoint;

	// Use this for initialization
	void Start () {
        HandleBirth();
	}
	
	// Update is called once per frame
	void Update () {
        HandleMovement();	
	}

    protected override void HandleBirth()
    {
        base.HandleBirth();
        m_startingPoint = m_rigidBody.position;
        m_endPoint = m_startingPoint + new Vector3(0, 0, m_length);
    }

    protected override void HandleMovement()
    {
        // This moves a neutral object side to side
        m_rigidBody.transform.position = Vector3.Lerp(m_startingPoint, m_endPoint, Mathf.PingPong(Time.time, 1));
    }
}
