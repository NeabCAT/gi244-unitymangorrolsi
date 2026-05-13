using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioSource pickupSound;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateScore(coinValue);
            StartCoroutine(PlaySoundThenReturn());
        }
    }

    IEnumerator PlaySoundThenReturn()
    {
        pickupSound?.Play();
        yield return new WaitForSeconds(pickupSound.clip.length);
        CoinPool.staticInstance.Return(this.gameObject);
    }
}
