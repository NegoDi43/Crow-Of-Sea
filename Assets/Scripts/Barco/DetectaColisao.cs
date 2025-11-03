using UnityEngine;

public class DetectaColisao : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject botaoEntrar;
    [SerializeField] private GameObject botaoSair;
    [SerializeField] private GameObject jogador;
    [SerializeField] private GameObject barco;
    [SerializeField] private Transform pontoDeSaida;

    private bool estaDentro = false;

    private PlayerController2D controleJogador;
    private BarcoController controleBarco;

    void Start()
    {
        botaoEntrar.SetActive(false);
        botaoSair.SetActive(false);

        controleJogador = jogador.GetComponent<PlayerController2D>();
        controleBarco = barco.GetComponent<BarcoController>();

        if (controleBarco != null)
            controleBarco.enabled = false; // barco começa desativado
    }

    public void EntrarNoBarco()
    {
        estaDentro = true;

        // Desativa controle do jogador e ativa o do barco
        controleJogador.enabled = false;
        controleBarco.enabled = true;

        // Teleporta o jogador para dentro do barco (invisível ou fixo)
        jogador.SetActive(false);

        botaoEntrar.SetActive(false);
        Debug.Log("Entrou no barco!");
    }

    public void SairDoBarco()
    {
        estaDentro = false;

        controleBarco.enabled = false;
        controleJogador.enabled = true;

        // Tira o jogador do barco
        jogador.transform.position = pontoDeSaida.position;
        jogador.SetActive(true);

        botaoSair.SetActive(false);
        Debug.Log("Saiu do barco!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !estaDentro)
            botaoEntrar.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            botaoEntrar.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PontoDeSaida") && estaDentro)
            botaoSair.SetActive(true);
    }
}
