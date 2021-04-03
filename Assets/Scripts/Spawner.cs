using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public WeightedList EnemyTypes;

    public float SpawnCount, RoundLength;

    private bool IsSpawning = false;
    private bool BossRound = false;

    private float SpawnRate;
    private float SpawnTime;
    private float EnemiesRemaining;
    
    private float[] InitialWeights;
    private float InitialSpawnCount, InitialRoundLength;

    void Start() {
        EnemyTypes.CalculateWeightTotal();

        InitialWeights = new float[EnemyTypes.Objects.Length];

        for (int i = 0; i < EnemyTypes.Objects.Length; i++) {
            InitialWeights[i] = EnemyTypes.Objects[i].Weight;
        }

        InitialSpawnCount  = SpawnCount;
        InitialRoundLength = RoundLength;
    }

    void Update() {
        if (IsSpawning && !Director.IsGameOver()) {
            if (EnemiesRemaining > 0) {
                SpawnTime += Time.deltaTime;

                if (SpawnTime >= SpawnRate) {
                    SpawnTime = 0.0f;

                    SpawnEnemy(EnemyTypes.GetRandomWeightedObject());

                    EnemiesRemaining--;
                }

            } else if (EnemiesRemaining == 0 && BossRound) {
                SpawnEnemy(EnemyTypes.Objects[3].Prefab);
                BossRound = false;

            } else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                Director.RoundFinished();
                IsSpawning = false;
            }
        } else if (Director.IsGameOver()) {
            IsSpawning = false;
        }
    }

    public void SpawnEnemy(GameObject enemy) {
        Vector2 location = Random.value >= 0.5 ? new Vector2(10, Random.Range(-10, 10)) : new Vector2(-10, Random.Range(-10, 10));
        
        Instantiate(enemy, location, Quaternion.identity);
    }

    public void ResetSpawner() {
        for (int i = 0; i < EnemyTypes.Objects.Length; i++) {
            EnemyTypes.Objects[i].Weight = InitialWeights[i];
        }

        SpawnCount  = InitialSpawnCount;
        RoundLength = InitialRoundLength;
    }

    public void NewRound(bool bossRound) {
        BossRound = bossRound;

        // Boosting enemy weights
        for (int i = 1; i < EnemyTypes.Objects.Length - 1; i++) {
            EnemyTypes.SetWeight(i, EnemyTypes.Objects[i].Weight + .5f);
        }

        SpawnCount += 10;
        EnemiesRemaining = SpawnCount;

        SpawnRate = RoundLength / SpawnCount;

        IsSpawning = true;
    }
}
