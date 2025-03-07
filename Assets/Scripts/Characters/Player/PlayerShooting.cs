using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject myCamera;
    private const int TYPE_BULLET = 0;
    public event Action OnBulletShoot;
    public int bulletsShot;

    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Shoot.performed += PlayerShoot;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Shoot.performed -= PlayerShoot;
    }

    private void PlayerShoot(InputAction.CallbackContext context) //
    {
        OnBulletShoot?.Invoke();
        bulletsShot++;

        GameObject newBulletGO = Instantiate(Bullet, myCamera.transform.position, myCamera.transform.rotation);
        Bullet bullet = newBulletGO.GetComponent<Bullet>();
        bullet.Shoot(TYPE_BULLET, myCamera.transform.position, myCamera.transform.forward);
        Physics.IgnoreCollision(GetComponent<CharacterController>(), newBulletGO.GetComponent<Collider>());
    }

    public void SetCamera(Transform camTransform)
    {
        myCamera = camTransform.gameObject;
    }
}
