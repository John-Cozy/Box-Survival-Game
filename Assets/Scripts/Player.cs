using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MoveSpeed = 0.01f;
    public int MaxHealth = 3;
    public SpriteRenderer SpriteRenderer;
    public bool IsDead = false;

    private int currentHealth;

    // Start is called before the first frame update
    void Start() {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update() {
        if (!IsDead) {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * MoveSpeed;
            transform.Translate(move);
        }
    }

    public void ResetPlayer() {
        IsDead = false;
        currentHealth = MaxHealth;
        SpriteRenderer.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            currentHealth--;

            SpriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)currentHealth / MaxHealth);

            if (currentHealth < 1) {
                Director.PlayerDied();
                IsDead = true;
            }
        }
    }
}
