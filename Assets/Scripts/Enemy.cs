using UnityEngine;
using UnityEngine.UI;

public class Enemy : Cube {

    public int ScoreValue     = 10;
    public float PickupChance = 5;

    public GameObject ExplosionPrefab;
    public GameObject TextPrefab;

    private Transform PlayerPosition;

    void Start() {
        health = MaxHealth;
        PlayerPosition = Player.GetPlayer().transform;
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            health--;

            Destroy(collision.gameObject);

            if (health == 0) {
                AudioManager.Play(DeadAudio);
                Director.AddToScore(ScoreValue);

                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

                if (StaticLibrary.RandomBool(PickupChance)) {
                    PickupManager.PlaceRandomPickup(transform.position);
                } else {
                    SpawnPoints();
                }

                Destroy(gameObject);
            } else {
                AudioManager.Play(HitAudio);
                UpdateColour();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    public void SpawnPoints() {
        Vector3 position = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position);
        GameObject points = Instantiate(TextPrefab, position, Quaternion.identity, GameObject.Find("Canvas").transform);

        points.GetComponent<Text>().text = "" + ScoreValue;
    }
}
