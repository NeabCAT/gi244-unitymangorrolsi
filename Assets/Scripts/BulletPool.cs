using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private readonly List<GameObject> bulletPool = new();
    public static BulletPool staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewBullet();
            if (i % 20 == 0) yield return null;
        }
    }

    private void CreateNewBullet()
    {
        var go = Instantiate(bulletPrefab, Vector3.one * -9999f, Quaternion.identity);
        go.SetActive(false);
        bulletPool.Add(go);
    }

    public GameObject Acquire(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        if (bulletPool.Count == 0)
            CreateNewBullet();

        var go = bulletPool[0];
        bulletPool.RemoveAt(0);

        go.transform.position = spawnPosition;
        go.transform.rotation = spawnRotation;

        go.SetActive(true);
        return go;
    }

    public void Return(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Add(bullet);
    }
}