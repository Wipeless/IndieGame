using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    public enum ExplosionType
    {
        ROCKET,
        MISSILE
    }
    public ExplosionType m_ExplosionType;

    public Material m_ExplosionMaterial;

    public int m_ExplosionStrength { get; private set; }
    private const float k_fadeRate = 0.05f;
    private float m_radius;
    private float m_lifespan = 0.5f;
    private Color m_originalColor;

	// Use this for initialization
	void Start () {

        GameObject storage = GameObject.Find("ExplosionStorage");
        if (storage != null)
        {
            //save the explosion to the ExplosionStorage object to keep things clean
            transform.parent = storage.transform;
        }

        // Save and reset this explosion's color
        m_originalColor = m_ExplosionMaterial.color;
        m_originalColor.a = 0.5f;

        switch (m_ExplosionType)
        {
            case ExplosionType.MISSILE:
                m_ExplosionStrength = XMLReader_GameProperties.ExplosionStrength_Missile;
                m_radius = XMLReader_GameProperties.ExplosionRadius_Missile;

                AudioManager.Instance.PlaySFX(AudioManager.Instance.Explosion_Missile,1);

                break;
            case ExplosionType.ROCKET:
                m_ExplosionStrength = XMLReader_GameProperties.ExplosionStrength_Rocket;
                m_radius = XMLReader_GameProperties.ExplosionRadius_Rocket;

                AudioManager.Instance.PlaySFX(AudioManager.Instance.Explosion_Rocket,1);
                break;
            default:
                Debug.Log("Unhandled explosion type: " + m_ExplosionType);
                break;
        }

        transform.localScale = new Vector3(m_radius, m_radius, m_radius);

        //Explosions will not have a rigid body.
        //On birth, mark them for death.
        Destroy(this.gameObject, m_lifespan);
	}
	
	// Update is called once per frame
	void Update () {

        m_originalColor.a -= k_fadeRate;
        if (m_originalColor.a < 0)
        {
            m_originalColor.a = 0;
        }

        m_ExplosionMaterial.color = m_originalColor;
	}
}
