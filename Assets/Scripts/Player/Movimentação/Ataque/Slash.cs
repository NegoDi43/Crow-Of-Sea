using UnityEngine;

public class Slash : MonoBehaviour
{
    public Status status;
    public float lifeTime = 0.25f;
    public LayerMask enemyLayer;

    private void Start()
    {
        status = FindAnyObjectByType<Status>();
        status = gameObject.GetComponent<Status>();
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;

        var enemy = other.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.TakeDamage(status.GetDanoMaximo());
        }
    }
}
