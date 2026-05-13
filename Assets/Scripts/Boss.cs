using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float followSpeed = 1.2f;

    [Header("Health")]
    public int maxHP = 5;
    private int currentHP;

    private Transform playerTransform;
    private PlayerController playerController;

    public System.Action<int, int> onHPChanged;
    public System.Action onBossDeath;

    public void Init(Transform player)
    {
        playerTransform = player;
        playerController = player.GetComponent<PlayerController>();

        currentHP = maxHP;
        onHPChanged?.Invoke(currentHP, maxHP);
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (playerController != null && playerController.gameOver)
        {
            Destroy(gameObject);
            return;
        }

        float targetY = playerTransform.position.y;
        float newY = Mathf.Lerp(transform.position.y, targetY, followSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (playerController != null)
        {
            AudioSource.PlayClipAtPoint(playerController.bossAudio, transform.position);
        }

        if (this != null)
        {
            onHPChanged?.Invoke(currentHP, maxHP);
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void DieFromCollision()
    {
        Die();
    }

    private void Die()
    {
        onBossDeath?.Invoke();

        onHPChanged = null;
        onBossDeath = null;

        Destroy(gameObject);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
