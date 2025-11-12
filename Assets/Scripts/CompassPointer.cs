using UnityEngine;

public class CompassPointer : MonoBehaviour
{
    [Header("Configurações da Bússola")]
    [Tooltip("Tag do objeto que a bússola deve apontar")]
    [SerializeField] string targetTag = "Barco";      // Tag do objeto a ser seguido

    [Tooltip("Transform do jogador (centro da bússola)")]
    [SerializeField] private Transform player;               // Referência ao jogador

    [Tooltip("Velocidade com que o ponteiro gira até o alvo")]
    [SerializeField] private float smoothSpeed = 5f;         // Suavização da rotação

    [SerializeField] private Transform target;              // Referência ao alvo

    void Start()
    {
        // Tenta encontrar o objeto com a tag especificada
        GameObject foundTarget = GameObject.FindGameObjectWithTag(targetTag);
        if (foundTarget != null)
        {
            target = foundTarget.transform;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag '" + targetTag + "' foi encontrado!");
        }
    }

    void Update()
    {
        // Garante que o player e o alvo existem
        if (target == null || player == null) return;

        // Calcula a direção do player até o alvo
        Vector2 direction = target.position - player.position;

        // Calcula o ângulo em graus a partir dessa direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Cria uma rotação alvo
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Faz o ponteiro rotacionar suavemente em direção ao alvo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
