using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++){
            GameObject InstancePrefab = Instantiate(prefab);
            InstancePrefab.transform.position = new Vector3(2 * i, Random.Range(0, 5), 0);
            InstancePrefab.transform.parent = this.transform;
            InstancePrefab.AddComponent<Rotate3DObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
