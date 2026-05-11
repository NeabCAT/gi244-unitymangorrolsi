using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class FlipCame : MonoBehaviour
{
    private static bool isFlipping = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFlipping) return;

            var player = other.GetComponent<PlayerController>();
            player.StartCoroutine(FlipEffect(player.cam.transform, player));
            ItemPool.Instance.Return(gameObject);
        }
    }

    IEnumerator FlipEffect(Transform cam, PlayerController player)
    {
        isFlipping = true;

        // บอกก่อนหมุน
        for (int i = 3; i >= 1; i--)
        {
            player.countdownText.text = i.ToString();
            player.countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        player.countdownText.text = "Flip!";
        yield return new WaitForSeconds(1f);
        player.countdownText.gameObject.SetActive(false);

        // หมุนกล้อง
        Quaternion startRot = cam.localRotation;
        cam.localRotation = Quaternion.Euler(0, 0, 180f);

        yield return new WaitForSeconds(5f);

        // บอกก่อนกลับปกติ
        for (int i = 3; i >= 1; i--)
        {
            player.countdownText.text = i.ToString();
            player.countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        player.countdownText.gameObject.SetActive(false);

        // กลับปกติ
        if (cam != null)
            cam.localRotation = startRot;

        isFlipping = false;
    }
}
