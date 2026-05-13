using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 8f;
    [SerializeField] private float yOffset = 0f;

    private Transform playerTransform;
    private PlayerController playerController;

    public void Init(Transform player)
    {
        playerTransform = player;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (playerController != null && playerController.gameOver)
        {
            GhostPool.staticInstance.Return(gameObject);
            return;
        }

        float targetY = playerTransform.position.y + yOffset;
        float newY = Mathf.Lerp(transform.position.y, targetY, followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null && !pc.noDamage)
            {
                pc.hp--;
                pc.Dead();
            }
            GhostPool.staticInstance.Return(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            GhostPool.staticInstance.Return(gameObject);
        }
    }
}