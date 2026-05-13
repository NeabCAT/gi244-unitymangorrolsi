using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class FlipCame : MonoBehaviour
{
    private static bool isFlipping = false;
    public AudioSource pickupSound;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFlipping) return;

            var player = other.GetComponent<PlayerController>();
            player.StartCoroutine(FlipEffect(player.cam.transform, player));

            if (pickupSound.clip != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
            }

            ItemPool.Instance.Return(gameObject);
        }
    }

    IEnumerator FlipEffect(Transform cam, PlayerController player)
    {
        isFlipping = true;

        // นับเวลาก่อนหมุน
        for (int i = 3; i >= 1; i--)
        {
            if (player.gameOver) { isFlipping = false; yield break; }
            player.countdownText.text = i.ToString();
            player.countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (player.gameOver) { isFlipping = false; yield break; }
        player.countdownText.text = "Flip!";
        yield return new WaitForSeconds(1f);
        player.countdownText.gameObject.SetActive(false);

        // หมุนกล้อง
        if (player.gameOver) { isFlipping = false; yield break; }
        Quaternion startRot = cam.localRotation;
        cam.localRotation = Quaternion.Euler(0, 0, 180f);

        float elapsed = 0f;
        while (elapsed < 5f)
        {
            if (player.gameOver)
            {
                cam.localRotation = startRot;
                isFlipping = false;
                yield break;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 3; i >= 1; i--)
        {
            if (player.gameOver)
            {
                cam.localRotation = startRot;
                isFlipping = false;
                yield break;
            }
            player.countdownText.text = i.ToString();
            player.countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        // กล้องกลับปกติ
        player.countdownText.gameObject.SetActive(false);
        if (cam != null)
            cam.localRotation = startRot;
        isFlipping = false;
    }
}
