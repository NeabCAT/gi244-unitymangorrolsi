using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int initialPoolSize = 5;

    private readonly List<GameObject> coinPool = new();
    public static CoinPool staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewCoin();
            if (i % 20 == 0) yield return null;
        }
    }

    private void CreateNewCoin()
    {
        var go = Instantiate(coinPrefab);
        go.SetActive(false);
        coinPool.Add(go);
    }

    public GameObject Acquire()
    {
        var go = coinPool.Find(o => !o.activeSelf);

        if (go == null)
        {
            CreateNewCoin();
            go = coinPool[coinPool.Count - 1];
        }

        coinPool.Remove(go);
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Add(coin);
    }
}
