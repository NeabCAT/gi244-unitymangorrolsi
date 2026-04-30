using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textCoinScore;
    public TextMeshProUGUI textEndScore;
    private int score;

    public Button retryButton;
    public Button mainMenuButton;
    public GameObject gameOverScreen;

    void Awake()
    {
        Instance = this;

        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Prototype");
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main Menu");
        });
    }

    public void UpdateScore(int s)
    {
        score += s;
        textCoinScore.text = $"{score}";
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        textCoinScore.gameObject.SetActive(false);
        textEndScore.text = $"Coin : {score}";
    }
}
