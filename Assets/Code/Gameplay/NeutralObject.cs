using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralObject : GameplayObject {

	// Use this for initialization
	void Start ()
    {
        HandleBirth();
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletTag")
        {
            // This neutral is being hit by a bullet.  Reduce its health by the strength of the bullet
            m_health -= collision.gameObject.GetComponent<BulletObject>().m_BulletStrength;

            if (m_health <= 0)
            {
                // This neutral has taken too much damage, kill it.
                HandleDeath();
            }
        }
    }

    protected override void HandleBirth()
    {
        //Give the neutral a random direction to walk to forever
        Vector2 randomDir = Random.insideUnitCircle;
        transform.forward = new Vector3(randomDir.x, 0, randomDir.y);

        base.HandleBirth();
    }

    protected override void HandleMovement()
    {
        m_rigidBody.AddForce(transform.forward * m_MovementForce);
    }

    protected override void HandleDeath()
    {
        // Kill this game object
        if (!m_isDying)
        {
            m_isDying = true;
            Destroy(this.gameObject);

            ScoreManager.AddScore(-45);

        }
    }
}
