using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Status base")]
    [SerializeField] private float vidaMaxima = 4;

    [Header("Status Mutaveis")]
    [SerializeField] private float danoMaximo = 1;
    [SerializeField] private float velocidade;
    [SerializeField] private float stamina = 100;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        limites();
    }

    private void limites()
        {
        if (vidaMaxima < 1)
        {
            vidaMaxima = 1;
        }
        if (danoMaximo < 1)
        {
            danoMaximo = 1;
        }
        if (velocidade < 1)
        {
            velocidade = 1;
        }
        if (stamina < 1)
        {
            stamina = 1;
        }
    }


    // Getters
    public float GetVidaMaxima()
    {
        return vidaMaxima;
    }
    public float GetDanoMaximo()
    {
        return danoMaximo;
    }
    public float GetVelocidade()
    {
        return velocidade;
    }
    public float GetStamina()
    {
        return stamina;
    }
}
