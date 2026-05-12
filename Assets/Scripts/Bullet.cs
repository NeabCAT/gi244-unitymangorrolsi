using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackPoint = 1;

    private void OnEnable()
    {
        Physics.IgnoreCollision(
            GetComponent<Collider>(),
            GameObject.FindWithTag("Player").GetComponent<Collider>()
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("Ground") ||
            other.CompareTag("Platformer")) return;

        // Ghost
        if (other.CompareTag("Ghost"))
        {
            Destroy(other.gameObject);

            BulletPool.staticInstance.Return(gameObject);

            return;
        }

        // Boss
        Boss boss = other.GetComponent<Boss>();

        if (boss != null)
        {
            boss.TakeDamage(attackPoint);

            BulletPool.staticInstance.Return(gameObject);

            return;
        }

        BulletPool.staticInstance.Return(gameObject);
    }
}