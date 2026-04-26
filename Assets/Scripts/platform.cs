using System.Collections;
using UnityEngine;

public class platform : MonoBehaviour
{
    private PlayerController playerController;

    public float speed = 5f;
    private float leftBound = -15;

    public GameObject platFromer;
    public GameObject spawnObstacles;
    private Vector3 startPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startPos = transform.position;
        StartCoroutine(PlatFrom());
    }

    // Update is called once per frame
    void Update()
    {
        if (!platFromer.activeSelf) return;

        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (!playerController.gameOver && playerController.isDash)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
        }
    }

    IEnumerator PlatFrom()
    {
        yield return new WaitForSeconds(5);

        while (true)
        {
            transform.position = startPos;
            platFromer.SetActive(true);
            yield return new WaitForSeconds(10);

            platFromer.SetActive(false);
            yield return new WaitForSeconds(20);
        }

    }
}
