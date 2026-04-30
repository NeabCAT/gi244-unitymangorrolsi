using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipCame : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(FlipRoutine(other));
        GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator FlipRoutine(Collider other)
    {
        Transform cam = other.GetComponent<PlayerController>().cam.transform;
        Quaternion startRot = cam.rotation;
        cam.rotation = Quaternion.Euler(0, 0, 180f);

        yield return new WaitForSeconds(5f);
        cam.rotation = startRot;
        Destroy(gameObject);
    }
}
