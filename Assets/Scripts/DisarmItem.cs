using UnityEngine;

public class DisarmItem : MonoBehaviour
{
    public float disarmDuration = 5f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.ApplyDisarm(disarmDuration); 
            }
            Destroy(gameObject);
        }
    }
}