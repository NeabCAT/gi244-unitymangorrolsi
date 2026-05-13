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
            pickupSound?.Play();
            CoinPool.staticInstance.Return(this.gameObject);
        }
    }
}
