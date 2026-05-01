using System.Collections;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.StartCoroutine(Boost(player));
            ItemPool.Instance.Return(gameObject);
        }
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
