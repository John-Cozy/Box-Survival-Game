using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float MoveVelocity = 0.02f;

    // Start is called before the first frame update
    void Start() {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = new Vector2(direction.x, direction.y);
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * MoveVelocity);
    }
}
