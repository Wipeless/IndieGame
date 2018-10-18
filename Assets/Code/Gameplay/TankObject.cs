using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankObject : GameplayObject
{

    public EnemyGunScript m_EnemyGun;

    private bool m_getsBonus;
    private bool m_explosionDeath;

    private const int k_enemyPoints = 1000;
    private const int k_bonusPoints = 1000;
    private const int k_explosionBonusPoints = 25;
    private const float k_range = 20;

    // Use this for initialization
    void Start()
    {
        HandleBirth();
        InvokeRepeating("HandleMovement", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void HandleBirth()
    {
        base.HandleBirth();
        m_health = 10000;
    }

    protected override void HandleMovement()
    {
        Vector3 playerPosition = FindObjectOfType<PlayerObject>().gameObject.transform.position;

        if (playerPosition == null)
        {
            Debug.LogError("Unable to find player in the scene");
        }

        transform.position = JumpWithinRange(transform.position, playerPosition);
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
                    m_getsBonus = bullet.m_hitWall;

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

    protected override void HandleDeath()
    {
        if (!m_isDying)
        {
            m_isDying = true;

            AudioManager.Instance.PlaySFX(AudioManager.Instance.EnemyTank_Death, 0.5f);

            for (int deathExplosionCount = 0; deathExplosionCount < 200; deathExplosionCount++)
            {
                Instantiate(m_DeathExplosion, transform.position, Quaternion.identity, null);
            }

            Destroy(this.gameObject);

            ScoreManager.AddScore(k_enemyPoints);
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

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        switch (otherObject.tag)
        {
            case "ExplosionTag":
                ExplosionObject explosion = otherObject.GetComponent<ExplosionObject>();

                m_health -= explosion.m_ExplosionStrength;

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

    private Vector3 JumpWithinRange(Vector3 origin, Vector3 player)
    {
        Vector2 randomPosition = Random.insideUnitCircle * k_range;
        origin.x = player.x + randomPosition.x;
        origin.z = player.z + randomPosition.y;

        return origin;
    }
}