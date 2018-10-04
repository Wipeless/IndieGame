using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour {

    public BulletObject m_BulletObjectPrefab;

    private Transform m_playerTransform;

    private const float k_maxFireRate = 0.6f;
    private const float k_minFireRate = 0.3f;
    private float m_fireRateLimit;
    private float m_fireRateTimer;

    public float m_FireRange = 20;  // Fire at the player if within this range;

    // Use this for initialization
    void Start ()
    {
        // Give some variety to how fast each enemy shoots
        m_fireRateLimit = Random.Range(k_minFireRate, k_maxFireRate);

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
        if (Time.time - m_fireRateTimer > m_fireRateLimit && 
            Vector3.Distance(transform.position, m_playerTransform.position) <= m_FireRange)
        {
            m_fireRateTimer = Time.time;
            Instantiate(m_BulletObjectPrefab, transform.position, transform.rotation);
        }
    }
}
