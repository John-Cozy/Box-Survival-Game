using UnityEngine;

public class Gun : MonoBehaviour {

    [System.Serializable]
    public class GunType {
        public string Name;
        public string SoundClipName;
        public GameObject BulletPrefab;
        public float FireRate;
        public int BulletNumber;
        public float DistanceBetween;
        public bool Spread;
        public float SpreadAngle;
    }

    public GunType[] GunTypes;
    
    private GunType CurrentGun;

    public float ResetRate = 10.0f;

    private float fireTime = 0.0f;
    private float resetTime = 0.0f;

    void Start() {
        CurrentGun = GunTypes[0];
    }

    void Update() {
        fireTime  += Time.deltaTime;
        resetTime += Time.deltaTime;

        if (Input.GetMouseButton(0) && !Director.IsGameOver() && fireTime > CurrentGun.FireRate) {
            fireTime = 0.0f;

            FireGun();
        }

        if (CurrentGun != GunTypes[0] && resetTime > ResetRate) {
            CurrentGun = GunTypes[0];
        }
    }

    public void ChangeGun(int NewGun) {
        CurrentGun = GunTypes[NewGun];
        resetTime = 0.0f;
    }

    private void FireGun() {
        AudioManager.Play(CurrentGun.SoundClipName);

        for (int i = 0; i < CurrentGun.BulletNumber; i++) {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            GameObject bullet = Instantiate(CurrentGun.BulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.up = new Vector2(direction.x, direction.y);

            if (CurrentGun.BulletNumber > 1) {
                float bulletIncrement = i + (CurrentGun.BulletNumber % 2 == 0 ? .5f : 0) - CurrentGun.BulletNumber / 2;

                bullet.transform.Translate(Vector2.right * bulletIncrement * CurrentGun.DistanceBetween);

                if (CurrentGun.Spread) {
                    bullet.transform.rotation *= Quaternion.Euler(0, 0, bulletIncrement * -CurrentGun.SpreadAngle);
                }
            }
        }
    }
}
