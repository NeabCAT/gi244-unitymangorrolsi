using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SlowmotionItem : MonoBehaviour
{
    public float slowTimeScale = 0.3f;
    public float duration = 5f;
    public float fadeOutTime = 0.5f;

    public AudioSource pickupSound;

        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) StartCoroutine(ApplySlowMo());
    }

    IEnumerator ApplySlowMo()
    {
        if (pickupSound != null && pickupSound.clip != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
        }

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

        ItemPool.Instance.Return(gameObject);
    }
}
