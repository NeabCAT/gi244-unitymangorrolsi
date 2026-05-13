using System.Collections;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int healAmount = 1;
    public int maxHP = 3;

    public AudioSource pickupSound;

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
            StartCoroutine(PlaySoundThenReturn());
        }
    }

    IEnumerator PlaySoundThenReturn()
    {
        pickupSound?.Play();
        yield return new WaitForSeconds(pickupSound.clip.length);
        ItemPool.Instance.Return(gameObject);
    }
}
