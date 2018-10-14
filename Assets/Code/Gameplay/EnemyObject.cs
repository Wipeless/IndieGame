using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : GameplayObject {

    public EnemyGunScript m_EnemyGun;

    private bool m_getsBonus;

    private const int k_bonusPoints = 100;

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
            BulletObject bullet = collision.gameObject.GetComponent<BulletObject>();

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

            ScoreManager.AddScore(20);
            if (m_getsBonus)
            {
                ScoreManager.AddScore(k_bonusPoints);
            }
        }
    }
}
