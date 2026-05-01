using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip crashSfx;
    private bool hasTriggered = false;

    public void ResetWall()
    {
        hasTriggered = false;
    }

    private void OnEnable()
    {
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.explosionParticle != null)
                Instantiate(player.explosionParticle, transform.position, Quaternion.identity);

            AudioSource audioSrc = other.GetComponent<AudioSource>();
            if (audioSrc != null && crashSfx != null)
                audioSrc.PlayOneShot(crashSfx);

            if (!player.noDamage) player.hp--;
            player.Dead();
        }

        platform plat = FindFirstObjectByType<platform>();
        if (plat != null)
        {
            plat.OnPlayerHitWall();
        }
    }
}
