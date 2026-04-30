using UnityEngine;

public class HP_UI : MonoBehaviour
{
    public PlayerController player;
    public GameObject[] hearts;

    private int lastHP;

    void Start()
    {
        lastHP = player.hp;
        UpdateHearts();
    }

    void Update()
    {
        if (player.hp != lastHP)
        {
            lastHP = player.hp;
            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < player.hp);
        }
    }
}
