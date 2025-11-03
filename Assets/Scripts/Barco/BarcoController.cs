using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BarcoController : MonoBehaviour
{
    [Header("Configurações do Barco")]
    public float velocidade = 5f;           // velocidade de movimento
    //public float rotacaoVelocidade = 100f;  // velocidade da rotação (timão)

    [Header("Referências")]
    public VirtualJoystick2D joystick;        // o mesmo joystick do jogador

    void FixedUpdate()
    {
        // Entrada do joystick
        float vertical = joystick.Vertical;
        float horizontal = joystick.Horizontal;

        //// Rotação (vira o leme)
        //rb.MoveRotation(rb.rotation  * horizontal * rotacaoVelocidade * Time.fixedDeltaTime);

        //// Movimento para frente (na direção que o barco está apontando)
        //Vector2 direcao = transform.up * vertical * velocidade * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + direcao);

        transform.Translate(new Vector2(horizontal, vertical) * velocidade);
    }
}
