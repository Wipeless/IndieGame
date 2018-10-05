using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {

    public PlayerObject m_PlayerObjectPrefab;
    public NeutralObject m_NeutralObjectPrefab;
    public EnemyObject m_EnemyObjectPrefab;

    public Transform m_NeutralObjectStorage;
    public Transform m_EnemyObjectStorage;

    public GameplayScreen m_GameplayScreen;

    private PlayerObject m_playerObject;
    private float m_gameplayStartTime;

    private int m_lastScore = 0;

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
    void Update ()
    {
        if (m_lastScore != ScoreManager.GetScore())
        {
            m_lastScore = ScoreManager.GetScore();
            m_GameplayScreen.SetIntelScore(m_lastScore);
        }
    }
}
