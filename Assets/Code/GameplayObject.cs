using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayObject : MonoBehaviour {

    protected int m_health = 100;
    protected Rigidbody m_rigidBody;
    protected float m_timeOfBirth;
    protected bool m_isDying = false;

    public float m_MovementForce;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    /// <summary>
    /// Handle the spawning of a game object
    /// </summary>
    protected virtual void HandleBirth()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_timeOfBirth = Time.time;
    }

    /// <summary>
    /// Handle the basic movement of a game object
    /// </summary>
    protected virtual void HandleMovement()
    {
    }

    /// <summary>
    /// Handle the death of a game object before it's removed from the scene.
    /// </summary>
    protected virtual void HandleDeath()
    {
        if (!m_isDying)
        {
            m_isDying = true;
            Destroy(this.gameObject);
        }
    }
}
