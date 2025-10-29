using UnityEngine;

public class DetectaColisao : MonoBehaviour
{
    [SerializeField] private GameObject butaoEntrar; // Botão para entrar no barco
    [SerializeField] private GameObject butaoSair; // Botão para sair no barco
    public GameObject jogador; // Referência ao jogador
    public GameObject barco; // Referência ao barco
    public GameObject pontoDeSaida; // Ponto onde o jogador sairá do barco
    private bool estahDentro = false; // Flag para verificar se o jogador esta no barco

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        butaoEntrar.SetActive(false);
        butaoSair.SetActive(false);
    }

    void Update()
    {
        if (estahDentro)
        {
            barco.transform.localRotation = Quaternion.identity; // Coloca a rotação do barco igual a do jogador
        }
    }

    public void EntrarNoBarco() // Lógica para entrar no barco
    {
        jogador.transform.SetParent(barco.transform); // Torna o jogador filho do barco
        jogador.transform.localPosition = Vector2.zero; // Posiciona o jogador no centro do barco
        jogador.transform.localRotation = Quaternion.identity; // Coloca a rotação do jogador igual a do barco
        jogador.transform.SetParent(null); // Remove a hierarquia de filho
        barco.transform.SetParent(jogador.transform); // Torna o barco filho do jogador
        barco.transform.localRotation = Quaternion.identity; // Coloca a rotação do barco igual a do jogador
        estahDentro = true;
        Debug.Log("Entrou no barco!");
    }

    public void SairDoBarco() // Lógica para sair do barco
    {
        barco.transform.SetParent(null); // Remove a hierarquia de filho
        jogador.transform.position = pontoDeSaida.transform.position; // Posiciona o jogador no ponto de saída
        estahDentro = false;
        Debug.Log("Saiu do barco!");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !estahDentro) // Toca no Player e ativa o botão
        {
            butaoEntrar.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoDeSaida") && estahDentro)
        {
            butaoSair.SetActive(true);
        }
    }
}
