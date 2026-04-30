using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] public GameObject[] itemPrefabs;
    [SerializeField] private int initialPoolSizePerPrefab = 5;

    private readonly List<GameObject> itemPool = new();
    public static ItemPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSizePerPrefab; i++)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            CreateNewItem(itemPrefabs[randomIndex]);

            if (i % 20 == 0) yield return null;
        }
    }

    private void CreateNewItem(GameObject prefab)
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        itemPool.Add(go);
    }

    public GameObject Acquire(GameObject prefab)
    {
        var go = itemPool.Find(o => !o.activeSelf && o.name == prefab.name + "(Clone)");

        if (go == null)
        {
            CreateNewItem(prefab);
            go = itemPool[itemPool.Count - 1];
        }

        itemPool.Remove(go);
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject item)
    {
        item.SetActive(false);
        itemPool.Add(item);
    }
}