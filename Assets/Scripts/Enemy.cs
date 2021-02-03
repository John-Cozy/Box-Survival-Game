using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float MoveSpeed = 0.1f;
    public int MaxHealth = 1;
    public int ScoreValue = 10;
    public Transform PlayerPosition;

    private int currentHealth;

    // Start is called before the first frame update
    void Start() {
        PlayerPosition = GameObject.Find("Player").transform;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update() {
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            currentHealth--;

            if (currentHealth < 1) {
                Director.AddToScore(ScoreValue);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
