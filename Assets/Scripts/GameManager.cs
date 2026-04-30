using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textCoinScore;
    private int score;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int s)
    {
        score += s;
        textCoinScore.text = $"{score}";
    }
}
