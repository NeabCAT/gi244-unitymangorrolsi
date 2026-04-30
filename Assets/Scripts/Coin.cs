using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateScore(coinValue);
            CoinPool.staticInstance.Return(this.gameObject);
        }
    }
}
