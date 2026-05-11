using UnityEngine;

public class BulletOutOfBound : MonoBehaviour
{
    [SerializeField] private float rightBound = 50f;

    private void Update()
    {
        if (transform.position.x > rightBound )
        {
            BulletPool.staticInstance.Return(gameObject);
        }
    }
}