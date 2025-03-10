using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour, IPunObservable
{
    private const int TYPE_BULLET = 0;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject myCamera;

    public int BulletsShot { get; private set; }
    public bool IsFiring { get; private set; }

    public event Action OnBulletShoot;

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
        playerControls.Player.Shoot.performed -= PlayerShoot;
        playerControls.Disable();
    }

    private void PlayerShoot(InputAction.CallbackContext context)
    {
        OnBulletShoot?.Invoke();
        BulletsShot++;
        IsFiring = true;

        GameObject newBulletGO = Instantiate(bulletPrefab, myCamera.transform.position, myCamera.transform.rotation);
        Bullet bullet = newBulletGO.GetComponent<Bullet>();
        bullet.Shoot(TYPE_BULLET, myCamera.transform.position, myCamera.transform.forward);

        Physics.IgnoreCollision(GetComponent<CharacterController>(), newBulletGO.GetComponent<Collider>());
    }

    public void SetCamera(Transform camTransform)
    {
        myCamera = camTransform.gameObject;
    }

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(IsFiring); // Share IsFiring property from this instance to the other players
        }
        else
        {
            IsFiring = (bool)stream.ReceiveNext(); // Receives info from the original instance to the instance viewed by the other players
        }
    }
    #endregion
}
