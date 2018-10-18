using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : GameplayObject {

    private enum GunSelection
    {
        MACHINEGUN = 0,
        ROCKET,
        MISSILE,
    }

    private GunSelection m_currentGunSelection = GunSelection.MACHINEGUN;

    public BulletObject m_MachineGunBulletObjectPrefab;
    public BulletObject m_MissileBulletObjectPrefab;
    public BulletObject m_RocketBulletObjectPrefab;
    public Transform m_BulletSpawnPoint;
    public Transform m_BulletStorage;

    private const float k_rotateSpeed = 1.5f;
    private float m_fireRateTimer;
    private Vector3 m_rotationYAxis = new Vector3(0, 0, 0);
    private SceneManagerScript m_sceneManager;
    private float m_fireRateLimit = 0;

    private int m_ammoCount = 100;

    // Use this for initialization
    void Start ()
    {
        UpdateFireRate();

        base.HandleBirth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleMovement();
	}

    protected override void HandleMovement()
    {
        HandlePlayerMovement();
        HandleWeaponSelections();
        HandleWeaponFiring();
    }

    public void SetSceneManager(SceneManagerScript val)
    {
        m_sceneManager = val;
        m_sceneManager.SetAmmo(m_ammoCount);
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_rigidBody.AddForce(transform.forward * m_MovementForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_rigidBody.AddForce(-transform.forward * m_MovementForce);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rotate(1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(-1);
        }
    }

    private void HandleWeaponSelections()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_currentGunSelection = GunSelection.MACHINEGUN;
            UpdateFireRate();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_currentGunSelection = GunSelection.ROCKET;
            UpdateFireRate();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_currentGunSelection = GunSelection.MISSILE;
            UpdateFireRate();
        }
    }
    
    /// <summary>
    /// Reset the fire rate with each new bullet selection
    /// </summary>
    private void UpdateFireRate()
    {
        switch (m_currentGunSelection)
        {
            //TODO: use the fire rate from the actual bullet being fired instead of looking at the XML doc
            case GunSelection.MACHINEGUN:
                m_fireRateLimit = XMLReader_GameProperties.BulletFireRate_MachineGun;
                break;
            case GunSelection.MISSILE:
                m_fireRateLimit = XMLReader_GameProperties.BulletFireRate_Missile;
                break;
            case GunSelection.ROCKET:
                m_fireRateLimit = XMLReader_GameProperties.BulletFireRate_Rocket;
                break;
            default:
                Debug.Log("Unhandled gun selection: " + m_currentGunSelection);
                break;
        }
    }


    private void HandleWeaponFiring()
    {
        // Handle weapon firing if there's ammo
        if (m_ammoCount > 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time - m_fireRateTimer > m_fireRateLimit)
                {
                    m_fireRateTimer = Time.time;

                    switch (m_currentGunSelection)
                    {
                        case GunSelection.MACHINEGUN:
                            Instantiate(m_MachineGunBulletObjectPrefab, m_BulletSpawnPoint.position, m_BulletSpawnPoint.rotation, m_BulletStorage);
                            AudioManager.Instance.PlaySFX(AudioManager.Instance.BulletFire_MachineGun, 1);
                            break;
                        case GunSelection.MISSILE:
                            Instantiate(m_MissileBulletObjectPrefab, m_BulletSpawnPoint.position, m_BulletSpawnPoint.rotation, m_BulletStorage);
                            AudioManager.Instance.PlaySFX(AudioManager.Instance.BulletFire_Missile, 1);
                            break;
                        case GunSelection.ROCKET:
                            Instantiate(m_RocketBulletObjectPrefab, m_BulletSpawnPoint.position, m_BulletSpawnPoint.rotation, m_BulletStorage);
                            AudioManager.Instance.PlaySFX(AudioManager.Instance.BulletFire_Rocket, 1);
                            break;
                        default:
                            Debug.Log("Unhandled gun selection: " + m_currentGunSelection);
                            break;
                    }
                    m_ammoCount--;

                    if (m_ammoCount <= 0)
                    {
                        // The player is now out of ammo
                        m_ammoCount = 0;
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.UI_NoAmmo,1);
                    }

                    // update UI
                    m_sceneManager.SetAmmo(m_ammoCount);
                }
            }
        }
    }

    private void Rotate(float y)
    {
        if (y != 0)
        {
            m_rotationYAxis.Set(0f, y, 0f);
            m_rotationYAxis = m_rotationYAxis.normalized * k_rotateSpeed;
            Quaternion deltaRotation = Quaternion.Euler(m_rotationYAxis);
            m_rigidBody.MoveRotation(transform.rotation * deltaRotation);
        }
    }

    protected override void HandleDeath()
    {
        throw new System.NotImplementedException();
    }
}
