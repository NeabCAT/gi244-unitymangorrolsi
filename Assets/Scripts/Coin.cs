using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    public AudioClip pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            GameManager.Instance.UpdateScore(coinValue);
            CoinPool.staticInstance.Return(this.gameObject);
        }
    }
}
