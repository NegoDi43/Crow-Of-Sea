using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações de Ataque")]
    [SerializeField] private VirtualJoystick2D aimJoystick;  // joystick de mira
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform attackPoint;           // ponto de spawn do corte na mão
    [SerializeField] private float slashDistance = 1f;
    [SerializeField] private float cooldown = 1f;

    private float lastAttackTime;
    private Vector2 aimDirection = Vector2.right;

    [Header("Audio")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private Audio audioPlayer;

    [Header("Animação")]
    [SerializeField] private PlayerController2D player;

    void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
    }
    void Update()
    {
        // Atualiza a direção da mira com base no joystick
        Vector2 input = aimJoystick != null ? aimJoystick.InputDirection : Vector2.zero;
        if (input.sqrMagnitude > 0.01f)
        {
            aimDirection = input.normalized;
        }
    }

    public void Attack()
    {
        if (Time.time < lastAttackTime + cooldown)
            return;

        StartCoroutine(AtacarTempo());
        
    }

    IEnumerator AtacarTempo()
    {
        player.AnimaAtacarCorte();
        yield return new WaitForSeconds(0.7f);

        lastAttackTime = Time.time;

        Vector2 spawnPos = (Vector2)attackPoint.position + aimDirection * slashDistance;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Instantiate(slashPrefab, spawnPos, Quaternion.Euler(0, 0, angle));
        audioPlayer.TocarSom(attackSound);
    }
}
