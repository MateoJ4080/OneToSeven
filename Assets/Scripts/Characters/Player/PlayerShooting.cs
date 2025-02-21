using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] GameObject myCamera;
    private const int TYPE_BULLET = 0;
    public event Action OnBulletShoot;
    public int bulletsShot;

    void Update()
    {
        PlayerShoot();
    }

    private void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // UI function
            OnBulletShoot?.Invoke();
            bulletsShot++;

            // Bullet prefab function
            GameObject newBulletGO = Instantiate(Bullet, myCamera.transform.position, myCamera.transform.rotation);
            Bullet bullet = newBulletGO.GetComponent<Bullet>();
            bullet.Shoot(TYPE_BULLET, myCamera.transform.position, myCamera.transform.forward);
            Physics.IgnoreCollision(GetComponent<Collider>(), newBulletGO.GetComponent<Collider>());
        }
    }
}
