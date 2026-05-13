using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 1;
    public int maxHP = 3;

    public AudioSource pickupSound;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && !player.gameOver)
            {
                player.hp += healAmount;
                if (player.hp > maxHP) player.hp = maxHP;
            }

            if (pickupSound.clip != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
            }

            ItemPool.Instance.Return(gameObject);
        }
    }
}
