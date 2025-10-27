using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public VirtualJoystick2D aimJoystick;  // joystick de mira
    public GameObject slashPrefab;
    public Transform attackPoint;           // ponto de spawn do corte na mão
    public float slashDistance = 1f;
    public float cooldown = 0.5f;

    private float lastAttackTime;
    private Vector2 aimDirection = Vector2.right;

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

        lastAttackTime = Time.time;

        Vector2 spawnPos = (Vector2)attackPoint.position + aimDirection * slashDistance;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Instantiate(slashPrefab, spawnPos, Quaternion.Euler(0, 0, angle));
    }
}
