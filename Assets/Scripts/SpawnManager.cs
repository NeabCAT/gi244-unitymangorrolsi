using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 spawnPos = new(25, 0, 0);
    public float coinOffsetX = 5f;
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        InvokeRepeating(nameof(SpawnCoin), startDelay, repeatRate);
    }

    private void SpawnObstacle()
    {
        if (playerController.gameOver) return;

        var prefabs = ObstaclePool.staticInstance.obstaclePrefabs;
        if (prefabs.Length == 0) return;

        int randomIndex = Random.Range(0, prefabs.Length);
        var go = ObstaclePool.staticInstance.Acquire(prefabs[randomIndex]);
        go.transform.position = spawnPos;
        go.transform.rotation = prefabs[randomIndex].transform.rotation;
    }

    private void SpawnCoin()
    {
        if (playerController.gameOver) return;

        var coin = CoinPool.staticInstance.Acquire();
        coin.transform.position = spawnPos + new Vector3(-coinOffsetX, (float)0.8, 0);
    }
}
