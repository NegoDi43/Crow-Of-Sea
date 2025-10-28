using UnityEngine;

public class Projetil : MonoBehaviour
{
    public Status status;            // Status do player (para calcular dano)
    public float lifeTime = 2f;      // Tempo de vida do projétil
    public LayerMask enemyLayer;     // Layer dos inimigos
    public float speed = 10f;        // Velocidade do projétil
    private Vector2 direction;       // Direção do disparo

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
  
    void Update()
    {
        if (direction != Vector2.zero)
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Rotaciona o sprite de acordo com a direção
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se atingiu um inimigo
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;

        var enemy = other.GetComponent<IDamageable>();
        if (enemy != null && status != null)
        {
            enemy.TakeDamage(status.GetDanoMaximo());
        }

        Destroy(gameObject);
    }
}
