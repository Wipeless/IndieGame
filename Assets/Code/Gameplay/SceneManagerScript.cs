using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {

    public PlayerObject m_PlayerObjectPrefab;
    public NeutralObject m_NeutralObjectPrefab;
    public EnemyObject m_EnemyObjectPrefab;
    public EnemyBuildingObject m_EnemyBuildingObjectPrefab;

    public Transform m_NeutralObjectStorage;
    public Transform m_EnemyObjectStorage;
    public Transform m_EnemyBuildingObjectStorage;

    public GameplayScreen m_GameplayScreen;

    private PlayerObject m_playerObject;
    private float m_gameplayStartTime;
    private float m_gameplayRemainingTime;

    private int m_lastScore = 0;
    private float m_fuelScore = 100;

    #region Const
    private const float k_fuelDecreaseRate = 0.01f;
    private const int k_timeLimit = 180;
    private const float k_spawnRangeMin = 30;
    private const float k_spawnRangeMax = 100;
    #endregion

    // Use this for initialization
    void Start ()
    {
        // Mark the start of the gameplay scene
        m_gameplayStartTime = Time.time;

        // Spawn player
        m_playerObject = Instantiate(m_PlayerObjectPrefab);

        // Make sure to spawn player in the air
        m_playerObject.transform.position += new Vector3(0, 5, 0);
        m_playerObject.SetSceneManager(this);

        // Spawn neutral objects
        for (int i = 0; i < 10; i++)
        {
            NeutralObject newNeutral = Instantiate(m_NeutralObjectPrefab, Vector3.zero, Quaternion.identity, m_NeutralObjectStorage);
            newNeutral.transform.position = RandomizeSpawn(newNeutral.transform.position);
        }

        // Spawn enemy objects
        for (int i = 0; i < 10; i++)
        {
            EnemyObject newEnemy = Instantiate(m_EnemyObjectPrefab, Vector3.zero, Quaternion.identity, m_EnemyObjectStorage);
            newEnemy.transform.position = RandomizeSpawn(newEnemy.transform.position);
        }

        EnemyBuildingObject newEnemyBuilding = Instantiate(m_EnemyBuildingObjectPrefab, Vector3.zero, Quaternion.identity, m_EnemyBuildingObjectStorage);
        newEnemyBuilding.transform.position = RandomizeSpawn(newEnemyBuilding.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        HandleScore();
        HandleFuel();
        HandleRemainingTime();
    }

    #region Public

    public void SetAmmo(int val)
    {
        m_GameplayScreen.SetAmmoScore(val);
    }

    #endregion

    #region Private

    /// <summary>
    /// Randomize the position of a 3D object on a 2D plane.
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    private Vector3 RandomizeSpawn(Vector3 origin)
    {
        float randomRadius = Random.Range(k_spawnRangeMin, k_spawnRangeMax);

        Vector2 randomPosition = Random.insideUnitCircle * randomRadius;
        origin.x += randomPosition.x;
        origin.z += randomPosition.y;

        return origin;
    }

    private void HandleScore()
    {
        if (m_lastScore != ScoreManager.GetScore())
        {
            m_lastScore = ScoreManager.GetScore();
            m_GameplayScreen.SetIntelScore(m_lastScore);
        }
    }

    private void HandleFuel()
    {
        m_fuelScore -= k_fuelDecreaseRate;
        m_GameplayScreen.SetFuelScore((int)m_fuelScore);
    }

    private void HandleRemainingTime()
    {
        float elapsedTime = Time.time - m_gameplayStartTime;
        float remainingTime = k_timeLimit - elapsedTime;
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        m_GameplayScreen.SetRemainingTime((int)(remainingTime / 60), (int)(remainingTime % 60));
    }

    #endregion
}
