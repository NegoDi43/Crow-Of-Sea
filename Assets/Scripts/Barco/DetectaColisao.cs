using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DetectaColisao : MonoBehaviour
{
    [Header("Referências UI")]
    [SerializeField] private GameObject botaoEntrar;
    [SerializeField] private GameObject botaoSair;
    [Header("Referências do Jogodor e do barco")]
    [SerializeField] private GameObject jogador;
    [SerializeField] private GameObject barco;
    [Header("Referências do ponto de saída do jogador")]
    [SerializeField] private Transform pontoDeSaida;

    public static bool estaDentro = false;

    private PlayerController2D controleJogador;
    private BarcoController controleBarco;
    private Animator animatorJogador;
    private Animator animatorBarco;

    void Start()
    {
        botaoEntrar.SetActive(false);
        botaoSair.SetActive(false);

        controleJogador = jogador.GetComponent<PlayerController2D>();
        controleBarco = barco.GetComponent<BarcoController>();
        animatorJogador = jogador.GetComponent<Animator>();
        animatorBarco = barco.GetComponent<Animator>();

        if (controleBarco != null)
            controleBarco.enabled = false; // barco começa desativado
    }

    public void EntrarNoBarco() // chamado pelo botão de entrar
    {
        estaDentro = true;
        // Desativa animação do jogador e ativa a do barco
        animatorJogador.enabled = false;
        animatorBarco.enabled = true;

        // Desativa controle do jogador e ativa o do barco
        controleJogador.enabled = false;
        controleBarco.enabled = true;

        // Teleporta o jogador para dentro do barco (invisível ou fixo)
        jogador.SetActive(false);

        botaoEntrar.SetActive(false);
        Debug.Log("Entrou no barco!");
    }

    public void SairDoBarco() // chamado pelo botão de sair
    {
        estaDentro = false;
        // Reativa animação do jogador e desativa a do barco
        animatorJogador.enabled = true;
        animatorBarco.enabled = false;

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
