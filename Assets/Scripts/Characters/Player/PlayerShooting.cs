using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : PlayerMovement
{
    [SerializeField] private GameObject Bullet;
    private const int TYPE_BULLET_PLAYER = 0;

    [SerializeField] GameObject myCamera;

    void Update()
    {
        PlayerShoot();
    }

    private void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBulletGO = Instantiate(Bullet, myCamera.transform.position, myCamera.transform.rotation);
            Bullet bullet = newBulletGO.GetComponent<Bullet>();
            bullet.Shoot(TYPE_BULLET_PLAYER, myCamera.transform.position, myCamera.transform.forward);
            Physics.IgnoreCollision(GetComponent<Collider>(), newBulletGO.GetComponent<Collider>());
        }
    }
}
