using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] public GameObject[] obstaclePrefabs;
    [SerializeField] private int initialPoolSizePerPrefab = 5;

    private readonly List<GameObject> obstaclePool = new();
    public static ObstaclePool staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSizePerPrefab; i++)
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            CreateNewObstacle(obstaclePrefabs[randomIndex]);
            if (i % 20 == 0) yield return null;
        }
    }

    private void CreateNewObstacle(GameObject prefab)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        obstaclePool.Add(go);
    }

    public GameObject Acquire(GameObject prefab)
    {
        var go = obstaclePool.Find(o => !o.activeSelf && o.name == prefab.name + "(Clone)");

        if (go == null)
        {
            CreateNewObstacle(prefab);
            go = obstaclePool[obstaclePool.Count - 1];
        }

        obstaclePool.Remove(go);
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject obstacle)
    {
        obstacle.SetActive(false);
        obstaclePool.Add(obstacle);
    }
}
