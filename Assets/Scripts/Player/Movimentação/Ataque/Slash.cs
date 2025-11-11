using UnityEngine;

public class Slash : MonoBehaviour
{
    public Status status;       // Status do player para calcular dano
    public float lifeTime = 0.5f;

    private void Start()
    {
        // Tenta pegar o Status do player
        status = FindAnyObjectByType<Status>();
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStatus enemy = other.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.ReceberDano(status.GetDanoMaximo());
        }
    }

}
