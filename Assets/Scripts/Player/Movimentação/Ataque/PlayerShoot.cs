using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private VirtualJoystick2D aimJoystick;  // Joystick de mira
    [SerializeField] private GameObject projectilePrefab;     // Prefab do projétil
    [SerializeField] private Transform firePoint;             // Ponto de spawn
    [SerializeField] private float cooldown = 0.5f;           // Tempo entre tiros
    [SerializeField] private Status playerStatus;          // Status do jogador

    [SerializeField] private float quantidadeMunicao = 10f;
    private float lastAttackTime;
    private Vector2 aimDirection = Vector2.right;
    private Status status;

    [Header("Audio")]
    [SerializeField] private AudioClip tiroSound;
    [SerializeField] private Audio audioPlayer;

    void Start()
    {
        status = FindAnyObjectByType<Status>();
        aimJoystick = FindAnyObjectByType<VirtualJoystick2D>();
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    void Update()
    {
        Vector2 input = aimJoystick != null ? aimJoystick.InputDirection : Vector2.zero;

        if (input.sqrMagnitude > 0.01f)
            aimDirection = input.normalized;
    }

    public void Attack()
    {
        if (quantidadeMunicao > 0)
        {
            if (Time.time < lastAttackTime + cooldown)
                return;

            lastAttackTime = Time.time;

            // Instancia o projétil e o configura
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projetil proj = projectile.GetComponent<Projetil>();
            audioPlayer.TocarSom(tiroSound);

            // Corrigido: diminui a stamina usando um setter apropriado
            playerStatus.SetStaminaAtual(2);
            // Supondo que exista um método para definir a stamina atual, por exemplo:
            // playerStatus.SetStaminaAtual(staminaAtual);
            // Se não existir, você deve criar esse método na classe Status.

            if (proj != null)
            {
                proj.status = status;
                proj.SetDirection(aimDirection);
            }
            else
            {
                Debug.LogWarning("O prefab do projétil não possui o script PlayerProjectile!");
            }
            quantidadeMunicao--;
        }
    }
}
