using UnityEngine;

public class HighScore : MonoBehaviour
{
    private const string KEY_COINS = "BestCoins";
    private const string KEY_DISTANCE = "BestDistance";

    public static HighScore Instance { get; private set; }

    public int BestCoins { get; private set; }
    public float BestDistance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BestCoins = PlayerPrefs.GetInt(KEY_COINS, 0);
        BestDistance = PlayerPrefs.GetFloat(KEY_DISTANCE, 0f);
    }

    public void Submit(int coins, float distance)
    {
        if (coins > BestCoins)
        {
            BestCoins = coins;
            PlayerPrefs.SetInt(KEY_COINS, BestCoins);
        }

        if (distance > BestDistance)
        {
            BestDistance = distance;
            PlayerPrefs.SetFloat(KEY_DISTANCE, BestDistance);
        }

        PlayerPrefs.Save();
    }
}
