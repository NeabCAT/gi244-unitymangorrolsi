using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Distance distance;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject bossPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float bossSpawnInterval = 1000f;

    private float nextSpawnDistance;

    private void Start()
    {
        nextSpawnDistance = bossSpawnInterval;
    }

    private void Update()
    {
        if (distance == null || player == null) return;

        if (distance.totalDistance >= nextSpawnDistance)
        {
            SpawnBossPrefabs();

            nextSpawnDistance += bossSpawnInterval;
        }
    }

    private void SpawnBossPrefabs()
    {
        GameObject bossObj = Instantiate(
            bossPrefab,
            new Vector3(player.position.x + 26f, player.position.y, 0f),
            Quaternion.identity
        );

        Boss boss = bossObj.GetComponent<Boss>();
        if (boss != null)
        {
            boss.Init(player);

            if (HPBoss.instance != null)
            {
                HPBoss.instance.ShowHpBoss(boss);
            }
        }
    }
}
