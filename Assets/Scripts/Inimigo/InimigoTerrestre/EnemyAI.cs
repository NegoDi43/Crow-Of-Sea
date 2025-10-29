using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private EnemyStatus statusEnemy;
    [SerializeField] private Rigidbody2D rb;
    private Transform player;

    [Header("Ataque")]
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        statusEnemy = GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoverAtePlayer();
        }
        else
        {
            PararMovimento();
            Atacar();
        }

    }

    void MoverAtePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * statusEnemy.GetVelocidade();
    }

    void PararMovimento()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Atacar()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        if (attackPrefab && attackPoint)
        {
            // Calcula a direção do player
            Vector2 direction = (player.position - attackPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Instancia o corte e rotaciona
            GameObject attackObj = Instantiate(attackPrefab, attackPoint.position, Quaternion.Euler(0, 0, angle));

            // Define a direção do ataque (para o script do corte)
            EnemyAttack attack = attackObj.GetComponent<EnemyAttack>();
            if (attack != null)
            {
                attack.SetDirection(direction);
            }
        }
    }

}
