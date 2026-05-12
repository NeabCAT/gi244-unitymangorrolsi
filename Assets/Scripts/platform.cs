using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class platform : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public GameObject platModel;
    public GameObject cam;

    [Header("Positions")]
    public Vector3 normalCamPos = new Vector3(8, 4, -15);
    public Vector3 highCamPos = new Vector3(8, 9.5f, -15);

    private PlayerController playerController;
    private SpawnManager spawnManager;
    private Vector3 startPos;
    private Vector3 originalSpawnPos;
    private Coroutine loopRoutine;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = FindFirstObjectByType<SpawnManager>();
        startPos = transform.position;
    }

    void Start()
    {
        originalSpawnPos = spawnManager.spawnPos;
        DeactivatePlatformState();
        RestartSystem(10f);
    }

    void Update()
    {
        if (platModel.activeSelf && !playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    public void RestartSystem(float firstDelay = 0f)
    {
        if (loopRoutine != null) StopCoroutine(loopRoutine);
        loopRoutine = StartCoroutine(PlatformLogicLoop(firstDelay));
    }

    IEnumerator PlatformLogicLoop(float firstDelay)
    {
        if (firstDelay > 0) yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (playerController.gameOver)
            {
                yield return null;
                continue;
            }

            ActivatePlatformState();

            float timer = 0;
            while (timer < 30f && !playerController.gameOver)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            DeactivatePlatformState();

            timer = 0;
            while (timer < 30f && !playerController.gameOver)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    void ActivatePlatformState()
    {
        transform.position = startPos;
        platModel.SetActive(true);
        Wall[] walls = GetComponentsInChildren<Wall>(true);
        foreach (Wall w in walls)
        {
            w.ResetWall();
        }
    }

    void DeactivatePlatformState()
    {
        platModel.SetActive(false);
        spawnManager.spawnPos = originalSpawnPos;
        cam.transform.position = normalCamPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                spawnManager.spawnPos = playerController.transform.position + new Vector3(25, 2.5f, 0);
                cam.transform.position = highCamPos;

                break;
            }
        }
    }

    public void OnPlayerHitWall()
    {
        DeactivatePlatformState();
        RestartSystem(5f);
    }

    public void OnBossSpawn()
    {
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);
            loopRoutine = null;
        }

        if (platModel != null)
        {
            platModel.SetActive(false);
        }
    }

    public void OnBossDefeated()
    {
        RestartSystem(2f);
    }
}
