using UnityEngine;

public class BarcoController : MonoBehaviour
{
    [Header("Configurações do Barco")]
    public float velocidade = 5f;           // velocidade de movimento
    public float rotacaoVelocidade = 100f;  // velocidade da rotação (timão)

    [Header("Referências")]
    public VirtualJoystick2D joystick;        // o mesmo joystick do jogador

    void FixedUpdate()
    {
        // Entrada do joystick
        float vertical = joystick.Vertical;
        float horizontal = joystick.Horizontal;

        Vector2 direcao = joystick.InputDirection.normalized;

        if (direcao.magnitude > 0.75f)
        {
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotacaoDesejada = Quaternion.Euler(0, 0, angulo);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoDesejada, rotacaoVelocidade * Time.fixedDeltaTime);
        }
        // Movimento do barco
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }
}
