using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject EnemyPrefab;

    public float SpawnRate;
    public float SpawnIncrementRate;
    public float SpawnIncrement;

    public bool IsSpawning = false;

    private float spawnTime = 0.0f;
    private float incrementTime = 0.0f;

    private float initialSpawnRate;

    public void ResetSpawner() {
        SpawnRate = initialSpawnRate;
    }

    // Start is called before the first frame update
    void Start() {
        initialSpawnRate = SpawnRate;
    }

    // Update is called once per frame
    void Update() {
        if (IsSpawning) {
            spawnTime += Time.deltaTime;

            if (spawnTime >= SpawnRate) {
                spawnTime = 0.0f;

                Vector2 location = Random.value >= 0.5 ? new Vector2(10, Random.Range(-10, 10)) : new Vector2(-10, Random.Range(-10, 10));

                Instantiate(EnemyPrefab, location, Quaternion.identity);
            }

            incrementTime += Time.deltaTime;

            if (incrementTime >= SpawnIncrementRate) {
                incrementTime = 0.0f;

                if (!(SpawnRate - SpawnIncrement < 0.01)) {
                    SpawnRate -= SpawnIncrement;
                }
            }
        }
    }


}
