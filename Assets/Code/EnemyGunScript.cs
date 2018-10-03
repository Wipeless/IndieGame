using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour {

    public BulletObject m_BulletObjectPrefab;

    private Transform m_playerTransform;

    private const float k_fireRateLimit = 0.5f;
    private float m_fireRateTimer;

    // Use this for initialization
    void Start ()
    {
        // Find the player's transform
        m_playerTransform = FindObjectOfType<PlayerObject>().gameObject.transform;

        if (m_playerTransform == null)
        {
            Debug.LogError("Unable to find player in the scene");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleGunLookAt();
        HandleGunFire();
	}

    private void HandleGunLookAt()
    {
        if (m_playerTransform != null)
        {
            this.transform.LookAt(m_playerTransform);
        }
    }

    private void HandleGunFire()
    {
        if (Time.time - m_fireRateTimer > k_fireRateLimit)
        {
            m_fireRateTimer = Time.time;
            Instantiate(m_BulletObjectPrefab, transform.position, transform.rotation);
        }
    }
}
