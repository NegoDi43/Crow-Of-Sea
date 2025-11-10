using System;
using System.Collections;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Status Base")]
    [SerializeField] private float vidaMaxima = 20f;
    [SerializeField] private float vidaAtual;

    [Header("Atributos de Combate")]
    [SerializeField] private float danoMaximo = 2f;
    [SerializeField] private float velocidade = 3f;

    [Header("Xp")]
    [SerializeField] private GanhodeXp ganhodeXp;

    public event Action<EnemyStatus> OnEnemyDeath;
    void Start()
    {
        vidaAtual = vidaMaxima;
        ganhodeXp = GameObject.FindGameObjectWithTag("Player").GetComponent<GanhodeXp>();
    }

    // Receber dano
    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} recebeu {dano} de dano! Vida restante: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        OnEnemyDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Reviver()
    {
        vidaAtual = vidaMaxima;
        gameObject.SetActive(true);
    }

    // Getters
    public float GetVidaMaxima()
        {
        return vidaMaxima;
    }
    public float GetVidaAtual()
        {
        return vidaAtual;
    }
    public float GetDanoMaximo() 
        {
        return danoMaximo;
    }
    public float GetVelocidade()
        {
        return velocidade;
    }

    // Setters (opcional: se quiser modificar durante o jogo)
    public void SetVidaMaxima(float value) 
        {
        vidaMaxima = value;
        if (vidaAtual > vidaMaxima)
            vidaAtual = vidaMaxima;
    }
    public void SetDanoMaximo(float value) 
        {
        danoMaximo = value;
    }
    public void SetVelocidade(float value) 
        {
        velocidade = value;
    }

    // Ganhar XP ao morrer
    private void OnDestroy()
    {
        if (ganhodeXp != null)
        {
            ganhodeXp.AdicionarXp(ganhodeXp.GetXpPorInimigo());
            Debug.Log($"Jogador ganhou {ganhodeXp.GetXpPorInimigo()} XP por matar {gameObject.name}.");
        }
    }
}
