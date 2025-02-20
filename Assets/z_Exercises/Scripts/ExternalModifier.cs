using UnityEngine;
using UnityEngine.UIElements;

public class ExternalModifier : MonoBehaviour
{
    public GameObject CubeToModify;
    void Start()
    {
        CubeToModify.GetComponent<Rotate3DObject>().RotationSpeed = 80;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
