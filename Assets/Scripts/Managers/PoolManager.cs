using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;
    private Queue<GameObject> bulletPool;

    [SerializeField] private Transform poolParent;

    void Start()
    {
        // Fill pool with bullets based on poolSize
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, poolParent);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet); // Add to the end of the Queue
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet); // Add to the end of the Queue
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue(); // Get bullet from the Queue front
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // If empty Queue, create new
            GameObject newBullet = Instantiate(bulletPrefab);
            return newBullet;
        }
    }
}
