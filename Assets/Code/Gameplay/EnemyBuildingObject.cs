using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuildingObject : GameplayObject {

    public EnemyGunScript m_EnemyGun;

    private bool m_explosionDeath;
    private bool m_getsBonus;

    private const int k_buildingPoints = 300;
    private const int k_explosionBonusPoints = 5;
    private const int k_bonusPoints = 100;

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
                BulletObject bullet = collisionObject.GetComponent<BulletObject>();

                // This is being hit by a bullet.  Reduce its health by the strength of the bullet
                m_health -= collisionObject.GetComponent<BulletObject>().m_BulletStrength;

                if (m_health <= 0)
                {
                    m_getsBonus = bullet.m_hitWall;

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
                    m_explosionDeath = true;
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
        m_health = XMLReader_GameProperties.EnemyHealth;

        base.HandleBirth();
    }

    protected override void HandleDeath()
    {
        if (!m_isDying)
        {
            m_isDying = true;
            Destroy(this.gameObject);

            ScoreManager.AddScore(k_buildingPoints);
            if (m_explosionDeath)
            {
                ScoreManager.AddScore(k_explosionBonusPoints);
            }
            if (m_getsBonus)
            {
                ScoreManager.AddScore(k_bonusPoints);
            }
        }
    }
}
