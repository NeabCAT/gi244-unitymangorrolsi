using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPool : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private int initialPoolSize = 5;

    private readonly List<GameObject> ghostPool = new();
    public static GhostPool staticInstance;

    private void Awake()
    {
        staticInstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewGhost();
            if (i % 20 == 0) yield return null;
        }
    }

    private void CreateNewGhost()
    {
        var go = Instantiate(ghostPrefab);
        go.SetActive(false);
        ghostPool.Add(go);
    }

    public GameObject Acquire()
    {
        var go = ghostPool.Find(o => !o.activeSelf);
        if (go == null)
        {
            CreateNewGhost();
            go = ghostPool[ghostPool.Count - 1];
        }
        ghostPool.Remove(go);
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject ghost)
    {
        ghost.SetActive(false);
        ghostPool.Add(ghost);
    }
}