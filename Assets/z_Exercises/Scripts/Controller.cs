using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Material myMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        { // Creates instances of prefab and randomizes position
            GameObject InstancePrefab = Instantiate(prefab);
            InstancePrefab.transform.position = new Vector3(2 * i, Random.Range(0, 5), 0);
            InstancePrefab.transform.parent = this.transform;
            InstancePrefab.AddComponent<Rotate3DObject>();
            if (IsVisibleFrom(prefab.transform.GetComponent<Renderer>().bounds, Camera.main))
            {
                Debug.Log("Instance [" + i + "] is visible");
            }
            else
            {
                Debug.Log("INSTANCE[" + i + "] is not visible");
            }
        }
        ApplyMaterialOnObjects(this.gameObject, myMaterial); // It's possible to use "this.gameObject.transform.root.gameObject to apply check every object in the scene, but I wont in case I add more objects later
    }
    // Update is called once per frame
    void Update()
    {

    }
    private bool IsVisibleFrom(Bounds _bounds, Camera _camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        return GeometryUtility.TestPlanesAABB(planes, _bounds);
    }
    private void ApplyMaterialOnObjects(GameObject _go, Material _material)
    {
        foreach (Transform child in _go.transform)
        {
            ApplyMaterialOnObjects(child.gameObject, _material);
        }
        if (_go.GetComponent<Renderer>() != null)
        {
            _go.GetComponent<Renderer>().material = _material;
        }
    }
}
