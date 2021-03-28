using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Cube {

    public Gun Gun;

    public bool IsDead = false;
    private bool godMode = false;

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
            UpdateHealth();
            godMode = false;
        }
    }

    public void ResetPlayer() {
        IsDead = false;
        health = MaxHealth;
        SpriteRenderer.color = Color.white;
        transform.position = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && !godMode) {
            if (IsDead) {
                AudioManager.Play("DeadHit");
            } else {
                health--;

                UpdateHealth();

                if (health > 0) {
                    AudioManager.Play(HitAudio);
                } else if (health == 0) {
                    AudioManager.Play(DeadAudio);
                    Director.PlayerDied();
                    IsDead = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Pickup")) {
            switch (collision.gameObject.GetComponent<Pickup>().PickupType) {
                case "Fire Rate":
                    Gun.FireRate *= 0.5f;
                    break;
                case "Health":
                    health++;
                    UpdateHealth();
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
