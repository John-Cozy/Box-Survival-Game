using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnRate;

    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= SpawnRate) {
            time = 0.0f;

            Vector2 location = Random.value >= 0.5 ? new Vector2(10, Random.Range(-10, 10)) : new Vector2(-10, Random.Range(-10, 10));

            Instantiate(EnemyPrefab, location, Quaternion.identity);
        }
    }
}
