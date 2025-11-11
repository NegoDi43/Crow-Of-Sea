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
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float yerDetectionRange = 10f;
    [SerializeField] private float attackCooldown = 1.5f;
    
    [Header("Audio")]
    [SerializeField] private Audio audioPlayer;
    [SerializeField] private AudioClip attackSound;

    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        statusEnemy = GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        animator = GetComponent<Animator>();
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
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) > yerDetectionRange)
        {
            PararMovimento();
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        animator.SetBool("Andando", false);
        animator.SetTrigger("Andar");
        rb.linearVelocity = direction * statusEnemy.GetVelocidade();
    }

    void PararMovimento()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("Andando", true);
        animator.SetTrigger("Parar");
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

            // Toca o som de ataque
            audioPlayer.TocarSom(attackSound);

            //Amação de ataque
            animator.SetTrigger("Atacar");

            // Define a direção do ataque (para o script do corte)
            EnemyAttack attack = attackObj.GetComponent<EnemyAttack>();
            if (attack != null)
            {
                attack.SetDirection(direction);
            }
        }
    }

}
