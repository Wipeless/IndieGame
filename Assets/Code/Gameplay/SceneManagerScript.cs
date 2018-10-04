using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {

    public PlayerObject m_PlayerObjectPrefab;
    public NeutralObject m_NeutralObjectPrefab;
    public EnemyObject m_EnemyObjectPrefab;

    public Transform m_NeutralObjectStorage;
    public Transform m_EnemyObjectStorage;

    private PlayerObject m_playerObject;
    private float m_gameplayStartTime;

	// Use this for initialization
	void Start ()
    {
        // Mark the start of the gameplay scene
        m_gameplayStartTime = Time.time;

        // Spawn player
        m_playerObject = Instantiate(m_PlayerObjectPrefab);
        // Make sure to spawn player in the air
        m_playerObject.transform.position += new Vector3(0, 5, 0);

        // Spawn neutral objects
        for (int i = 0; i < 10; i++)
        {
            Instantiate(m_NeutralObjectPrefab, new Vector3(0, 0, i * 2), Quaternion.identity, m_NeutralObjectStorage);
        }

        // Spawn enemy objects
        for (int i = 0; i < 10; i++)
        {
            Instantiate(m_EnemyObjectPrefab, new Vector3(i * 2, 0, -5), Quaternion.identity, m_EnemyObjectStorage);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
