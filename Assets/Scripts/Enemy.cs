using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Cube {
    public int ScoreValue     = 10;
    public float PickupChance = 5;

    public GameObject ExplosionPrefab;
    public GameObject[] PickupPrefabs;

    private Transform PlayerPosition;

    // Start is called before the first frame update
    void Start() {
        health = MaxHealth;
        PlayerPosition = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            health--;

            Destroy(collision.gameObject);

            if (health < 1) {
                AudioManager.Play(DeadAudio);
                Director.AddToScore(ScoreValue);

                GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().numOfPoints = ScoreValue;

                if (StaticLibrary.RandomBool(PickupChance)) {
                    Instantiate(PickupPrefabs[StaticLibrary.RandomChoice(PickupPrefabs.Length)], transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            } else {
                AudioManager.Play(HitAudio);
                UpdateHealth();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
