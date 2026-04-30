using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;
    public static float speedBoost = 1f;

    private float leftBound = -15;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }

        if (playerController.gameOver) return;

        transform.Translate(
            Vector3.left * Time.deltaTime * speed * speedBoost,
            Space.World
        );

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            ObstaclePool.staticInstance.Return(this.gameObject);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Coin"))
        {
            CoinPool.staticInstance.Return(this.gameObject);
        }
    }
}
