using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : GameplayObject {

    public EnemyGunScript m_EnemyGun;

	// Use this for initialization
	void Start ()
    {
        base.HandleBirth();
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
            // This enemy is being hit by a bullet.  Reduce its health by the strength of the bullet
            m_health -= collision.gameObject.GetComponent<BulletObject>().BulletStrength;

            if (m_health <= 0)
            {
                // This enemy has taken too much damage, kill it.
                HandleDeath();
            }
        }
    }

    protected override void HandleBirth()
    {
    }

    protected override void HandleMovement()
    {

    }

    protected override void HandleDeath()
    {
         // Kill this game object
        if (!m_isDying)
        {
            m_isDying = true;
            Destroy(this.gameObject);
        }
    }
}
