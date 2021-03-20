using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float MoveSpeed  = 0.1f;
    public int MaxHealth    = 1;
    public int ScoreValue   = 10;

    public GameObject ExplosionPrefab;

    private Transform PlayerPosition;
    private int currentHealth;

    // Start is called before the first frame update
    void Start() {
        PlayerPosition = GameObject.Find("Player").transform;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            currentHealth--;

            Destroy(collision.gameObject);

            if (currentHealth < 1) {
                AudioManager.Play("EnemyKilled");
                Director.AddToScore(ScoreValue);
                GetComponent<ParticleSystem>().Play();
                GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().numOfPoints = ScoreValue;
                Destroy(gameObject);
            } else {
                AudioManager.Play("EnemyHit");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
