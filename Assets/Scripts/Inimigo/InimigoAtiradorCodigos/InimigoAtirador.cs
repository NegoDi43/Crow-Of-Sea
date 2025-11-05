using UnityEngine;

public class inimigoAtirador : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private EnemyStatus statusEnemy;       // Status do inimigo
    private Rigidbody2D rb;           // Rigidbody2D do inimigo
    private Transform player;         // Player

    [Header("Tiro")]
    [SerializeField] private GameObject projectilePrefab; // Prefab do projétil
    [SerializeField] private Transform firePoint;         // Ponto de spawn do projétil
    [SerializeField] private float attackRange = 10f;      // Distância mínima para atirar
    [SerializeField] private float attackCooldown = 1.5f; // Cooldown entre tiros

    [Header("Audio")]
    [SerializeField] private Audio audioPlayer;
    [SerializeField] private AudioClip attackSound;

    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        statusEnemy = GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
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
        rb.linearVelocity = direction * statusEnemy.GetVelocidade();

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

            // Toca o som de ataque
            audioPlayer.TocarSom(attackSound);

            TiroInimigo projScript = proj.GetComponent<TiroInimigo>();
            if (projScript != null)
                projScript.SetDirection(direction, statusEnemy.GetDanoMaximo());
        }
    }

}
