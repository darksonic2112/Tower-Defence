using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public static int EnemiesAlive = 0;
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Transform destinationPoint;
    public TextMeshProUGUI waveCountdownText;
    public GameManager gameManager;
    public float timeBetweenWaves = 20f;
    private float countdown = 5f;
    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Transform enemyTransform = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation).transform;
        
        NavMeshAgent agent = enemyTransform.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(destinationPoint.position);
        }
    }
}