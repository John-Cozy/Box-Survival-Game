using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour {
    public GameObject BulletPrefab;
    public float FireRate;

    private float fireTime = 0.0f;

    // Update is called once per frame
    void Update() {
        fireTime += Time.deltaTime;

        if (Input.GetMouseButton(0) && !Director.IsGameOver() && fireTime > FireRate) {
            fireTime = 0.0f;

            AudioManager.Play("Fire");
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
