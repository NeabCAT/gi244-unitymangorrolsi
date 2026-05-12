using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static History;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public History historyPanel;

    public TextMeshProUGUI textCoinScore;
    public TextMeshProUGUI textEndScore;
    public TextMeshProUGUI textDistance;
    public TextMeshProUGUI textBestCoin;
    public TextMeshProUGUI textBestDistance;

    private int score;

    public Button retryButton;
    public Button mainMenuButton;
    public GameObject gameOverScreen;

    private Distance distance;

    void Awake()
    {
        Instance = this;
        distance = FindFirstObjectByType<Distance>();

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
        textDistance.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        textCoinScore.gameObject.SetActive(false);
        textEndScore.text = $"{score}";
        distance.Text();
        HighScore.Instance.Submit(score, distance.totalDistance);
        textBestCoin.text = $"{HighScore.Instance.BestCoins}";
        textBestDistance.text = $"{HighScore.Instance.BestDistance:F1} M ";

        historyPanel.AddRecord(score, distance.totalDistance);
    }
}
