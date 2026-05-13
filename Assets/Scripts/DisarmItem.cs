using UnityEngine;

public class DisarmItem : MonoBehaviour
{
    public float disarmDuration = 5f; 
    public AudioSource pickupSound;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.ApplyDisarm(disarmDuration); 
            }

            if (pickupSound.clip != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
            }

            ItemPool.Instance.Return(gameObject);
        }
    }
}