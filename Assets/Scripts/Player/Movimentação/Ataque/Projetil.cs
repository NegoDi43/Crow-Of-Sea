using UnityEngine;

public class Projetil : MonoBehaviour
{
    public Status status;           // Status do player (para pegar o dano)
    public float lifeTime = 2f;     // Tempo de vida do projétil
    public LayerMask enemyLayer;    // Layer dos inimigos
    public float speed = 10f;       // Velocidade do projétil
    private Vector2 direction;      // Direção do projétil

    void Start()
    {
        status = FindAnyObjectByType<Status>();
        status = gameObject.GetComponent<Status>();
        // Destrói o projétil após o tempo definido
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move o projétil na direção definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Define a direção do projétil após ser instanciado
    /// </summary>
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com a layer correta
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;

        // Aplica dano se o alvo for danificável
        var enemy = other.GetComponent<IDamageable>();
        if (enemy != null && status != null)
        {
            enemy.TakeDamage(status.GetDanoMaximo());
        }

        // Destrói o projétil ao atingir algo
        Destroy(gameObject);
    }
}

