using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float MoveVelocity = 0.02f;
    public float DeathTime = 5f;

    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, DeathTime);
    }

    private void FixedUpdate() {
        transform.Translate(Vector3.up * MoveVelocity);
    }
}
