using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Referências")]
    public VirtualJoystick2D aimJoystick;   // Joystick usado para mirar
    public GameObject projectilePrefab;     // Prefab do projétil
    public Transform firePoint;             // Ponto de origem do tiro (ex: cano da arma)

    [Header("Atributos do Tiro")]
    public float projectileSpeed = 10f;     // Velocidade do projétil
    public float cooldown = 0.4f;           // Tempo entre disparos

    private float lastShootTime;
    private Vector2 aimDirection = Vector2.right;

    void Update()
    {
        // Atualiza direção de mira conforme o joystick
        Vector2 input = aimJoystick != null ? aimJoystick.InputDirection : Vector2.zero;

        if (input.sqrMagnitude > 0.01f)
            aimDirection = input.normalized; // Normaliza direção para manter consistência
    }

    /// <summary>
    /// Chamada pelo botão de tiro (UI Button -> OnClick)
    /// </summary>
    public void Shoot()
    {
        // Verifica cooldown
        if (Time.time < lastShootTime + cooldown)
            return;

        lastShootTime = Time.time;

        // Instancia o projétil no ponto de disparo
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Define a direção e rotação do projétil
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Adiciona movimento ao projétil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = aimDirection * projectileSpeed;
    }
}
