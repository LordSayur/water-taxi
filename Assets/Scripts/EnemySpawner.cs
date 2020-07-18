using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<WaveConfig> waveConfigs;
    int staringWave = 0;

    private void Start()
    {
        var currentWave = waveConfigs[staringWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var enemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetPathPrefab()[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
