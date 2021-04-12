using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPost : MonoBehaviour {

    public GameObject[] GunPickups;
    private GameObject[] VisiblePickups;

    private bool ShouldSpawn = true;
    private SpriteRenderer Renderer;

    private void Start() {
        Renderer = GetComponent<SpriteRenderer>();
        VisiblePickups = new GameObject[3];
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        Destroy(collision.gameObject);
        AudioManager.Play("EnemyHit");

        if (collision.gameObject.CompareTag("Bullet") && ShouldSpawn) {
            InstantiatePickups();

            Tutorial.ChangePrompt();

            ShouldSpawn = false;
            StartCoroutine(AllowSpawning());

            Renderer.color = new Color(0.5f, 0, 0);
        }
    }

    IEnumerator AllowSpawning() {
        yield return new WaitForSeconds(4);
        ShouldSpawn = true;
        Renderer.color = new Color(0.2830189f, 0.2830189f, 0.2830189f);
    }

    private void InstantiatePickups() {
        Vector2 position = new Vector2(6, 0);

        for (int i = 0; i < GunPickups.Length; i++) {
            if (VisiblePickups[i]) Destroy(VisiblePickups[i]);

            VisiblePickups[i] = Instantiate(GunPickups[i], position, Quaternion.identity);
            position.y -= 1.5f;
        }
    }

    public void SelfDestruct() {
        foreach (GameObject pickup in VisiblePickups) {
            if (pickup) Destroy(pickup);
        }

        Destroy(gameObject);
    }
}
