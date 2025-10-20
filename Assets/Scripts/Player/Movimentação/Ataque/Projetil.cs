using UnityEngine;

public class Projetil : MonoBehaviour
{
    public Status status;           // Status do player (para pegar o dano)
    public float lifeTime = 2f;     // Tempo de vida do proj�til
    public LayerMask enemyLayer;    // Layer dos inimigos
    public float speed = 10f;       // Velocidade do proj�til
    private Vector2 direction;      // Dire��o do proj�til

    void Start()
    {
        status = FindAnyObjectByType<Status>();
        status = gameObject.GetComponent<Status>();
        // Destr�i o proj�til ap�s o tempo definido
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move o proj�til na dire��o definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Define a dire��o do proj�til ap�s ser instanciado
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

        // Aplica dano se o alvo for danific�vel
        var enemy = other.GetComponent<IDamageable>();
        if (enemy != null && status != null)
        {
            enemy.TakeDamage(status.GetDanoMaximo());
        }

        // Destr�i o proj�til ao atingir algo
        Destroy(gameObject);
    }
}

