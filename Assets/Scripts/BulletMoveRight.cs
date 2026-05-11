using UnityEngine;

public class BulletMoveRight : MonoBehaviour
{
    public float speed = 20f;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.World);
    }
}