using System.Collections;
using UnityEngine;

public class platform : MonoBehaviour
{
    private PlayerController playerController;

    public float speed = 2f;

    public GameObject platFromer;
    private SpawnManager spawnManager;

    private Vector3 startPos;
    private Vector3 originalSpawnPos;

    public GameObject cam;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = FindFirstObjectByType<SpawnManager>();

        startPos = transform.position;
        originalSpawnPos = spawnManager.spawnPos;

        StartCoroutine(PlatFrom());
    }

    void Update()
    {
        if (!platFromer.activeSelf) return;

        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    IEnumerator PlatFrom()
    {
        yield return new WaitForSeconds(5);

        while (!playerController.gameOver)
        {
            transform.position = startPos;
            platFromer.SetActive(true);
            yield return new WaitForSeconds(30);

            platFromer.SetActive(false);
            spawnManager.spawnPos = originalSpawnPos;
            yield return new WaitForSeconds(30);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnManager.spawnPos = playerController.transform.position + new Vector3(25, 2.5f, 0);
            cam.transform.position = new Vector3(8, 9.5f, -15);
        }
    }
}
