using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MoveSpeed = 0.01f;
    public int Health = 3;
    public SpriteRenderer SpriteRenderer;
    public bool IsDead = false;

    private bool godMode = false;
    private int currentHealth;

    // Start is called before the first frame update
    private void Start() {
        currentHealth = Health;
    }

    // Update is called once per frame
    private void Update() {
        if (!IsDead) {
            if (Input.GetKeyDown(KeyCode.G)) {
                ToggleGodmode();
            }
        }
    }

    private void FixedUpdate() {
        if (!IsDead) {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * MoveSpeed;
            transform.Translate(move);

            KeepPlayerInBounds();
        }
    }

    private void KeepPlayerInBounds() {
        Vector2 position = Camera.main.WorldToViewportPoint(transform.position);
        
        if (position.x < 0.0) transform.position += new Vector3(1, 0, 0);
        if (position.x > 1.0) transform.position -= new Vector3(1, 0, 0);
        if (position.y < 0.0) transform.position += new Vector3(0, 1, 0);
        if (position.y > 1.0) transform.position -= new Vector3(0, 1, 0);
    }

    private void ToggleGodmode() {
        if (!godMode) {
            AudioManager.Play("GodmodeOn");
            SpriteRenderer.color = Color.blue;
            godMode = true;
        } else {
            AudioManager.Play("GodmodeOff");
            SpriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)currentHealth / Health);
            godMode = false;
        }
    }

    public void ResetPlayer() {
        IsDead = false;
        currentHealth = Health;
        SpriteRenderer.color = Color.white;
        transform.position = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && !godMode) {
            currentHealth--;

            SpriteRenderer.color = Color.Lerp(Color.red, Color.white, (float) currentHealth / Health);

            if (currentHealth >= 0) {
                AudioManager.Play("PlayerHit");
            } else {
                AudioManager.Play("DeadHit");
            }

            if (currentHealth == 0) {
                Director.PlayerDied();
                IsDead = true;
            }
        }
    }
}
