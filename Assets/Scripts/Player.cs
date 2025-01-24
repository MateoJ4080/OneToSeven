using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private float _speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
    {
        float axisVertical = Input.GetAxis("Vertical");
        float axisHorizontal = Input.GetAxis("Horizontal");
        Vector3 forward = axisVertical * playerCamera.transform.forward * _speed * Time.deltaTime;
        Vector3 lateral = axisHorizontal * playerCamera.transform.right * _speed * Time.deltaTime;
        Vector3 increment = forward + lateral;
        transform.GetComponent<Rigidbody>().MovePosition(transform.position + increment);
    }
}
