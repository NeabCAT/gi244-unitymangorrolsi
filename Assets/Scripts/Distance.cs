using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI distanceTextEnd;
    [SerializeField] PlayerController player;
    private float totalDistance = 0f;
    private bool isGameOver = false;

    void Start()
    {
        if (distanceText != null)
        {
            distanceText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (player == null || player.gameOver)
        {
            return; 
        }

        if (!distanceText.gameObject.activeSelf)
        {
            distanceText.gameObject.SetActive(true);
        }

        totalDistance += Time.deltaTime * 10f;
        distanceText.text = "Distance " + totalDistance.ToString("F1") + " Meter";
    }

    public void Text()
    {
        distanceTextEnd.text = "Distance : " + totalDistance.ToString("F1") + " Meter";
    }
}
