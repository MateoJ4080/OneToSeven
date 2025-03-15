using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject myCamera;
    private PhotonView photonView;

    public int BulletsShot { get; private set; }
    public bool IsFiring { get; private set; }

    private PlayerControls playerControls;
    private PoolManager poolManager;


    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerControls = new PlayerControls();
        poolManager = FindAnyObjectByType<PoolManager>();

        if (photonView.IsMine)
        {
            myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
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
        IsFiring = true;

        GameObject bullet = poolManager.GetBullet();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Shoot(myCamera.transform.position, myCamera.transform.forward);

        Physics.IgnoreCollision(GetComponent<CharacterController>(), bullet.GetComponent<Collider>());
    }

    public void SetCamera(Transform camTransform)
    {
        myCamera = camTransform.gameObject;
    }

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
}
