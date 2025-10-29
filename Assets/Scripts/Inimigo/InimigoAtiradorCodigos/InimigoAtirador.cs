using UnityEngine;

public class inimigoAtirador : MonoBehaviour
{
    [Header("Componentes")]
    public EnemyStatus status;       // Status do inimigo
    public Rigidbody2D rb;           // Rigidbody2D do inimigo
    public Transform player;         // Player

    [Header("Tiro")]
    public GameObject projectilePrefab; // Prefab do projétil
    public Transform firePoint;         // Ponto de spawn do projétil
    public float attackRange = 5f;      // Distância mínima para atirar
    public float attackCooldown = 1.5f; // Cooldown entre tiros

    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Se estiver longe, move em direção ao player
        if (distance > attackRange)
            MoverAtePlayer();
        else
        {
            PararMovimento();
            Atirar();
        }
    }

    void MoverAtePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * status.GetVelocidade();

        // Opcional: vira o inimigo na direção do movimento
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    void PararMovimento()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Atirar()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        if (projectilePrefab && firePoint)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

            TiroInimigo projScript = proj.GetComponent<TiroInimigo>();
            if (projScript != null)
                projScript.SetDirection(direction, status.GetDanoMaximo());
        }
    }
}
