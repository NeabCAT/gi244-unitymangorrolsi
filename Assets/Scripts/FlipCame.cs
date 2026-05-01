using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipCame : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();

            player.StartCoroutine(FlipEffect(player.cam.transform));

            ItemPool.Instance.Return(gameObject);
        }
    }

    IEnumerator FlipEffect(Transform cam)
    {
        Quaternion startRot = cam.localRotation;
        cam.localRotation = Quaternion.Euler(0, 0, 180f);

        yield return new WaitForSeconds(5f);

        if (cam != null)
            cam.localRotation = startRot;
    }
}
