using System.Collections;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    private PlayerController player;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.GetComponent<PlayerController>();

        StartCoroutine(Boost());
        GetComponent<Renderer>().enabled = false;
    }

    IEnumerator Boost()
    {

        MoveLeft.speedBoost = 2f;
        player.noDamage = true;
        yield return new WaitForSeconds(5);

        MoveLeft.speedBoost = 1f;
        player.noDamage = false;
        gameObject.SetActive(false);
    }
}
