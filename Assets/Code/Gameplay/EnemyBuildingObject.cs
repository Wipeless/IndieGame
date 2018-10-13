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
       if (collision.gameObject.tag == "BulletTag")
        {
            m_health -= collision.gameObject.GetComponent<BulletObject>().m_BulletStrength;

            if (m_health <= 0)
            {
                HandleDeath();
            }
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
