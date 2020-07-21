using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy Wave Config", menuName = "Water Taxi/Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject pathPrefab = null;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    public GameObject GetEnemyPrefab() => enemyPrefab;
    public List<Transform> GetPathPrefab()
    {
        List<Transform> waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() => timeBetweenSpawns;
    public float GetSpawnRandonFactor() => spawnRandomFactor;
    public int GetNumberOfEnemies() => numberOfEnemies;
    public float GetMoveSpeed() => moveSpeed;
    public float GetRotationSpeed() => rotationSpeed;
}
