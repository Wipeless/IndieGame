using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : GameplayObject {

    public EnemyGunScript m_EnemyGun;

    private bool m_getsBonus;
    private bool m_explosionDeath;

    private const int k_EnemyPoints = 20;
    private const int k_bonusPoints = 100;
    private const int k_explosionBonusPoints = 5;

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
                BulletObject bullet = collisionObject.GetComponent<BulletObject>();

                // This enemy is being hit by a bullet.  Reduce its health by the strength of the bullet
                m_health -= bullet.m_BulletStrength;

                if (m_health <= 0)
                {
                    // Player gets bonus for killing blow bouncing bullet off wall first
                    if (bullet.m_hitWall)
                    {
                        m_getsBonus = true;
                    }

                    // This enemy has taken too much damage, kill it.
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
                ExplosionObject explosion = otherObject.GetComponent<ExplosionObject>();

                // This enemy is being hit by a explosion.  Reduce its health by the strength of the explosion
                m_health -= explosion.m_ExplosionStrength;

                if (m_health <= 0)
                {
                    m_explosionDeath = true;
                    // This enemy has taken too much damage, kill it.
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
        //Give the enemy a random direction to walk to forever
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

            ScoreManager.AddScore(k_EnemyPoints);
            if (m_getsBonus)
            {
                ScoreManager.AddScore(k_bonusPoints);
            }
            if (m_explosionDeath)
            {
                ScoreManager.AddScore(k_explosionBonusPoints);
            }
        }
    }
}
