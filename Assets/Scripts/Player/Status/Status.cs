using System.Collections;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Status base")]
    [SerializeField] private float vidaMaxima = 15;
    [SerializeField] private float vidaAtual;

    [Header("Status Mutaveis")]
    [SerializeField] private float danoMaximo = 1;
    [SerializeField] private float velocidade = 5;

    [Header("Stamina")]
    [SerializeField] private float staminaTotal;
    [SerializeField] private float staminaAtual;
    [SerializeField] private float staminaMax;

    [SerializeField] private float pontos;

    [Header("Status Pontos")]
    [SerializeField] private float pontosVida = 0;
    [SerializeField] private float pontosDano = 0;
    [SerializeField] private float pontosVelocidade = 0;
    [SerializeField] private float pontosStamina = 0;

    [SerializeField] private PlayerController2D playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AumentaVida());
        StartCoroutine(AumentaStamina());
        vidaAtual = vidaMaxima;
        staminaAtual = staminaMax;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        limites();
        AtualizaStamina();
        AtualizaVida();
    }

    //Atualiza os Status
    private void AtualizaStamina()
    {
        staminaTotal = staminaMax;
        
        if (staminaAtual > staminaMax)
        {
            staminaAtual = staminaMax;
        }
    }

    IEnumerator AumentaStamina()
    {
        yield return new WaitForSeconds(1);
        if (staminaAtual < staminaMax)
        {
            staminaAtual += 1;
        }

        StartCoroutine(AumentaStamina());
    }

    private void AtualizaVida()
    {
        if (vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }
    }

    IEnumerator AumentaVida()
    {
        yield return new WaitForSeconds(2);
        if (vidaAtual < vidaMaxima)
        {
            vidaAtual += 1;
            AtualizaVida();
        }
        StartCoroutine(AumentaVida());
    }

    // Aumenta os status

    public void UpVidaMaxima()
    {
        if (pontos > 0)
        {
            vidaMaxima += 2; 
            pontos -= 1;
            pontosVida += 2;
        }
    }
    public void UpDanoMaximo()
    {
        if (pontos > 0)
        {
            danoMaximo += (float)0.10; 
            pontos -= 1;
            pontosDano += 1;
        }

        
    }
    public void UpVelocidade()
    {
        if (pontos > 0)
        {
            velocidade += (float)0.10;
            pontos -= 1;
            pontosVelocidade += 1;
        }
    }
    public void UpStamina()
    {
        if (pontos > 0)
        {
            staminaMax += 5;
            pontos -= 1;
            pontosStamina += 1;
        }
    }

    // Limites para os status
    private void limites()
        {
        if (vidaMaxima < 1)
        {
            vidaMaxima = 0;
        }
        if (danoMaximo < 1)
        {
            danoMaximo = 1;
        }
        if (velocidade < 1)
        {
            velocidade = 1;
        }
        if (staminaAtual < 1)
        {
            staminaAtual = 0;
        }
    }

    // Correr
    public void Correndo()
    {
        if (playerController.Correndo(true))
        {
            staminaAtual -= 0.5f;
            velocidade += 2;
        }
        else
        {
            velocidade -= 2;
        }
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
    public float GetStaminaMax()
    {
        return staminaMax;
    }
    public float GetStaminaAtual()
    {
        return staminaAtual;
    }
    public float GetPontos()
    {
        return pontos;
    }
    public float GetPontosVida()
    {
        return pontosVida;
    }
    public float GetPontosDano()
    {
        return pontosDano;
    }
    public float GetPontosVelocidade()
    {
        return pontosVelocidade;
    }
    public float GetPontosStamina()
    {
        return pontosStamina;
    }

    // Ganhar Pontos
    public void GanharPontos()
    {
        pontos += 3;
    }

    // Receber Dano
    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            Destroy(gameObject); // ou animação de morte
        }
    }

}
