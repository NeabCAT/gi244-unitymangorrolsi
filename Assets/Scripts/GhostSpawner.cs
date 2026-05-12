using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float spawnX = 25f;
    [SerializeField] private float spawnInterval = 5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        var ghost = GhostPool.staticInstance.Acquire();

        ghost.transform.position = new Vector3(spawnX, player.position.y, 0f);
        ghost.GetComponent<GhostController>().Init(player);
    }
}