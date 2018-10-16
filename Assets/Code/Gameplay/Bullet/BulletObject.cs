using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : GameplayObject {

    private const int k_bulletLifespan = 1;

    public int m_BulletStrength = 100; // how much damage it does to an object
    public float m_FireForce;
    public bool m_hitWall { get; private set; }

    public ExplosionObject m_Explosion;

    // Use this for initialization
    void Start ()
    {
        base.HandleBirth();

        m_rigidBody.AddForce(transform.forward * m_FireForce);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the bullet has been alive for longer than its lifespan, kill it
        if (Time.time - m_timeOfBirth > k_bulletLifespan)
        {
            HandleDeath();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        // If a bullet collides with anything except for a wall, kill it
        if (collision.gameObject.tag != "WallTag")
        {
            // Instantiate an explosion if this bullet has one.

            if (m_Explosion != null)
            {
                Instantiate(m_Explosion, transform.position, transform.rotation, null);
            }

            HandleDeath();
        }
        else
        {
            m_hitWall = true; 
        }
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
    }
}
