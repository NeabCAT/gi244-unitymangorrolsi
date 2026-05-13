using System.Collections;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    public AudioSource pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.StartCoroutine(Boost(player));
            StartCoroutine(PlaySoundThenReturn());
        }
    }

    IEnumerator PlaySoundThenReturn()
    {
        pickupSound?.Play();
        yield return new WaitForSeconds(pickupSound.clip.length);
        ItemPool.Instance.Return(gameObject);
    }

    IEnumerator Boost(PlayerController player)
    {
        MoveLeft.speedBoost = 2f;
        if (player) player.noDamage = true;

        yield return new WaitForSeconds(5);

        MoveLeft.speedBoost = 1f;
        if (player) player.noDamage = false;
    }
}
