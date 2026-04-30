using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public AudioSource audioSource;
    public AudioClip countSound;  
    public AudioClip goSound;     

    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        int[] numbers = { 5, 4, 3, 2, 1 };

        foreach (int n in numbers)
        {
            countdownText.text = n.ToString();
            if (audioSource && countSound)
                audioSource.PlayOneShot(countSound); 
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "GO!";
        if (audioSource && goSound)
            audioSource.PlayOneShot(goSound);
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
