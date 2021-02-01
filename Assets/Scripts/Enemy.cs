using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public float MoveSpeed = 0.1f;
    public Transform PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, step);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
