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

    public float SpawnCount;
    public float SpawnRate;
    public float RoundLength;

    private bool isSpawning = false;

    private float spawnTime = 0.0f;

    private float totalSpawnWeight;

    private float enemiesToSpawn;

    public void ResetSpawner() {

    }

    private void CalculateTotalWeight() {
        totalSpawnWeight = 0;

        foreach (EnemyType e in EnemyTypes) {
            totalSpawnWeight += e.SpawnWeight;
        }
    }

    public void NewRound() {
        int enemyToBoost = Random.Range(1, EnemyTypes.Length);
        EnemyTypes[enemyToBoost].SpawnWeight += 1;
        CalculateTotalWeight();

        SpawnCount += 10;
        enemiesToSpawn = SpawnCount;

        SpawnRate = RoundLength / SpawnCount;

        isSpawning = true;
    }

    // Start is called before the first frame update
    void Start() {
        CalculateTotalWeight();
    }

    // Update is called once per frame
    void Update() {
        if (isSpawning) {
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

                enemiesToSpawn--;
            }

            if (enemiesToSpawn <= 0) {
                Director.RoundFinished();
                isSpawning = false;
            }
        }
    }


}
