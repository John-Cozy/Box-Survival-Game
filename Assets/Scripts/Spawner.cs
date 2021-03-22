using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [System.Serializable]
    public struct EnemyType {
        public GameObject EnemyPrefab;
        public float SpawnWeight;
    }

    public EnemyType[] EnemyTypes;

    public float SpawnCount, SpawnRate, RoundLength;

    private bool isSpawning = false;
    private float spawnTime = 0.0f;
    private float enemiesRemaining;

    private float totalSpawnWeight;
    
    private float[] initialWeights;
    private float initialSpawnRate, initialSpawnCount, initialRoundLength;

    public void ResetSpawner() {
        for (int i = 0; i < EnemyTypes.Length; i++) {
            EnemyTypes[i].SpawnWeight = initialWeights[i];
        }

        SpawnRate = initialSpawnRate;
        SpawnCount = initialSpawnCount;
        RoundLength = initialRoundLength;
    }

    private void CalculateTotalWeight() {
        totalSpawnWeight = 0;

        foreach (EnemyType e in EnemyTypes) {
            totalSpawnWeight += e.SpawnWeight;
        }
    }

    public void NewRound() {

        // Boosting enemy weights
        for (int i = 1; i < EnemyTypes.Length; i++) {
            EnemyTypes[i].SpawnWeight++;
        }

        CalculateTotalWeight();

        SpawnCount += 10;
        enemiesRemaining = SpawnCount;

        SpawnRate = RoundLength / SpawnCount;

        isSpawning = true;
    }

    void Start() {
        CalculateTotalWeight();

        initialWeights = new float[EnemyTypes.Length];

        for (int i = 0; i < EnemyTypes.Length; i++) {
            initialWeights[i] = EnemyTypes[i].SpawnWeight;
        }

        initialSpawnRate = SpawnRate;
        initialSpawnCount = SpawnCount;
        initialRoundLength = RoundLength;
    }

    void Update() {
        if (isSpawning && !Director.IsGameOver()) {
            if (enemiesRemaining > 0) {
                spawnTime += Time.deltaTime;

                if (spawnTime >= SpawnRate) {
                    spawnTime = 0.0f;

                    Vector2 location = Random.value >= 0.5 ? new Vector2(10, Random.Range(-10, 10)) : new Vector2(-10, Random.Range(-10, 10));

                    GameObject enemy = null;

                    float enemyWeightPosition = Random.Range(0, totalSpawnWeight);
                    float currentWeightPosition = 0;

                    foreach (EnemyType e in EnemyTypes) {
                        currentWeightPosition += e.SpawnWeight;

                        if (currentWeightPosition > enemyWeightPosition) {
                            enemy = e.EnemyPrefab;
                            break;
                        }
                    }

                    Instantiate(enemy, location, Quaternion.identity);

                    enemiesRemaining--;
                }

            } else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                Director.RoundFinished();
                isSpawning = false;
            }
        } else if (Director.IsGameOver()) {
            isSpawning = false;
        }
    }


}
