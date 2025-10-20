using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Refer�ncias")]
    public VirtualJoystick2D aimJoystick;   // Joystick usado para mirar
    public GameObject projectilePrefab;     // Prefab do proj�til
    public Transform firePoint;             // Ponto de origem do tiro (ex: cano da arma)

    [Header("Atributos do Tiro")]
    public float projectileSpeed = 10f;     // Velocidade do proj�til
    public float cooldown = 0.4f;           // Tempo entre disparos

    private float lastShootTime;
    private Vector2 aimDirection = Vector2.right;

    void Update()
    {
        // Atualiza dire��o de mira conforme o joystick
        Vector2 input = aimJoystick != null ? aimJoystick.InputDirection : Vector2.zero;

        if (input.sqrMagnitude > 0.01f)
            aimDirection = input.normalized; // Normaliza dire��o para manter consist�ncia
    }

    /// <summary>
    /// Chamada pelo bot�o de tiro (UI Button -> OnClick)
    /// </summary>
    public void Shoot()
    {
        // Verifica cooldown
        if (Time.time < lastShootTime + cooldown)
            return;

        lastShootTime = Time.time;

        // Instancia o proj�til no ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Define a dire��o e rota��o do proj�til
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Adiciona movimento ao proj�til
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = aimDirection * projectileSpeed;
    }
}
