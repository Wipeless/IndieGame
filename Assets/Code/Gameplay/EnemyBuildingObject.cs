using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuildingObject : GameplayObject {

    public EnemyGunScript m_EnemyGun;

	// Use this for initialization
	void Start ()
    {
        HandleBirth();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "BulletTag":
                // This is being hit by a bullet.  Reduce its health by the strength of the bullet
                m_health -= collisionObject.GetComponent<BulletObject>().m_BulletStrength;

                if (m_health <= 0)
                {
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
                m_health -= otherObject.gameObject.GetComponent<ExplosionObject>().m_ExplosionStrength;

                if (m_health <= 0)
                {
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
        m_health = 3000;
        base.HandleBirth();
    }

    protected override void HandleDeath()
    {
        if (!m_isDying)
        {
            m_isDying = true;
            Destroy(this.gameObject);
            ScoreManager.AddScore(300);
        }
    }
}
