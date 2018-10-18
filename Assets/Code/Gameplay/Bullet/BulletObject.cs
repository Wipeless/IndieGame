using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : GameplayObject {

    public enum BulletType
    {
        MACHINEGUN,
        ROCKET,
        MISSILE,
    }

    public BulletType m_BulletType;
   
    public float m_BulletFireRate { get; private set; }
    public int m_BulletStrength { get; private set; } // how much damage it does to an object
    public bool m_hitWall { get; private set; }

    public ExplosionObject m_Explosion;

    private float m_fireForce;
    private int m_bulletLifespan;

    // Use this for initialization
    void Start ()
    {
        switch (m_BulletType)
        {
            case BulletType.MACHINEGUN:
                m_BulletStrength = XMLReader_GameProperties.BulletStrength_MachineGun;
                m_BulletFireRate = XMLReader_GameProperties.BulletFireRate_MachineGun;
                m_fireForce = XMLReader_GameProperties.BulletFireForce_MachineGun;
                m_bulletLifespan = XMLReader_GameProperties.BulletLifeSpan_MachineGun;
                break;
            case BulletType.MISSILE:
                m_BulletStrength = XMLReader_GameProperties.BulletStrength_Missile;
                m_BulletFireRate = XMLReader_GameProperties.BulletFireRate_Missile;
                m_fireForce = XMLReader_GameProperties.BulletFireForce_Missile;
                m_bulletLifespan = XMLReader_GameProperties.BulletLifeSpan_Missile;
                break;
            case BulletType.ROCKET:
                m_BulletStrength = XMLReader_GameProperties.BulletStrength_Rocket;
                m_BulletFireRate = XMLReader_GameProperties.BulletFireRate_Rocket;
                m_fireForce = XMLReader_GameProperties.BulletFireForce_Rocket;
                m_bulletLifespan = XMLReader_GameProperties.BulletLifeSpan_Rocket;
                break;
            default:
                Debug.Log("Unhandled bullet type: " + m_BulletType);
                break;
        }

        base.HandleBirth();

        m_rigidBody.AddForce(transform.forward * m_fireForce);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the bullet has been alive for longer than its lifespan, kill it
        if (Time.time - m_timeOfBirth > m_bulletLifespan)
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
            Debug.Log("It hit the wall!");
            m_hitWall = true; 
        }
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
    }
}
