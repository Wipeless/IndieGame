using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    public Material m_ExplosionMaterial;
    public float m_Radius;
    public int m_ExplosionStrength = 200;

    private const float k_fadeRate = 0.05f;
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

        transform.localScale = new Vector3(m_Radius, m_Radius, m_Radius);

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
