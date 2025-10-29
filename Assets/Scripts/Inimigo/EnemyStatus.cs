using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour
{
    [Header("Status Base")]
    [SerializeField] private float vidaMaxima = 20f;
    [SerializeField] private float vidaAtual;

    [Header("Atributos de Combate")]
    [SerializeField] private float danoMaximo = 2f;
    [SerializeField] private float velocidade = 3f;

    void Start()
    {
        vidaAtual = vidaMaxima;
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
        Debug.Log($"{gameObject.name} morreu!");
        Destroy(gameObject);
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
}
