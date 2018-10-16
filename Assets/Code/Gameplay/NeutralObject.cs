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
        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "BulletTag":
                // This neutral is being hit by a bullet.  Reduce its health by the strength of the bullet
                m_health -= collisionObject.GetComponent<BulletObject>().m_BulletStrength;

                if (m_health <= 0)
                {
                    // This neutral has taken too much damage, kill it.
                    HandleDeath();
                }
                break;
            case "GroundTag":
                break;
            case "WallTag":
                break;
            default:
                Debug.Log("Encountered an unhandled tag: " + collisionObject.tag);
                break;
        }
    }

    /// <summary>
    /// Handle triggers.  Triggers are collisions with objects without rigid bodies such as boundaries and switches.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        switch (otherObject.tag)
        {
            case "ExplosionTag":
                // This neutral is being hit by an explosion.  Reduce its health by the strength of the explosion
                m_health -= otherObject.GetComponent<ExplosionObject>().m_ExplosionStrength;

                if (m_health <= 0)
                {
                    // This neutral has taken too much damage, kill it.
                    HandleDeath();
                }
                break;
            default:
                Debug.Log("Encountered an unhandled tag: " + otherObject.tag);
                break;
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
