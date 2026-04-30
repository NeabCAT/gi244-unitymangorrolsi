using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distanceText;
    private float totalDistance = 0f;

    void Start()
    {
        if (distanceText != null)
            distanceText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (!distanceText.gameObject.activeSelf)
            distanceText.gameObject.SetActive(true);

        totalDistance += Time.deltaTime * 10f;
        distanceText.text = "Distance " + totalDistance.ToString("F1") + " Meter";
    }
}
