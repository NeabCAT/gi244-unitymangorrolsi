using System.Collections;
using UnityEngine;
using TMPro;

public class SlowmotionItem : MonoBehaviour
{
    public AudioSource pickupSound;

    public float slowTimeScale = 0.3f;
    public float duration = 5f;
    public float fadeOutTime = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) StartCoroutine(ApplySlowMo());
    }

    IEnumerator ApplySlowMo()
    {
        GetComponent<Renderer>().enabled = GetComponent<Collider>().enabled = false;

        var txt = GameObject.Find("Timenumbertext")?.GetComponent<TextMeshProUGUI>();

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        for (float t = duration; t > 0; t -= Time.unscaledDeltaTime)
        {
            if (txt) { txt.text = t.ToString("F1") + "s"; }
            yield return null;
        }

        while (Time.timeScale < 1f)
        {
            Time.timeScale += Time.unscaledDeltaTime / fadeOutTime;
            yield return null;
        }

        Time.timeScale = 1f;
        if (txt) txt.text = "";
        StartCoroutine(PlaySoundThenReturn());
    }

    IEnumerator PlaySoundThenReturn()
    {
        pickupSound?.Play();
        yield return new WaitForSeconds(pickupSound.clip.length);
        ItemPool.Instance.Return(gameObject);
    }
}
