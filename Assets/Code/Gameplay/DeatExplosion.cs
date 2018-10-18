using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatExplosion : MonoBehaviour {

    public Rigidbody m_RigidBody;

    private const float k_deathExplosionForce = 1000;
    private const float k_deathExplosionTorqueForce = 1000;
    private const float k_deathTimeLimit = 2; // in seconds

	// Use this for initialization
	void Start () {
        GameObject storage = GameObject.Find("ExplosionStorage");
        if (storage != null)
        {
            //save the explosion to the ExplosionStorage object to keep things clean
            transform.parent = storage.transform;
        }

        // generate a random xz direction.
        Vector2 XZdirection = Random.insideUnitCircle;

        // apply the direction along with a random y intensity
        Vector3 startingForce = new Vector3(XZdirection.x, Random.Range(0.2f, 1f), XZdirection.y);

        // normalize just to be safe
        startingForce.Normalize();

        // apply force to the starting force
        m_RigidBody.AddForce(startingForce * k_deathExplosionForce);

        // apply a random spin
        Vector3 startingTorque = Random.insideUnitSphere * Random.Range(0.2f, 1f);
        m_RigidBody.AddTorque(startingTorque * k_deathExplosionTorqueForce);

        Destroy(this.gameObject, k_deathTimeLimit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
