using UnityEngine;

public class Obstatcle : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObstaclePool.staticInstance.Return(this.gameObject);
        }
    }
}
