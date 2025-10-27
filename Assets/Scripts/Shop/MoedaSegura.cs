using UnityEngine;

[System.Serializable]
public class MoedaSegura
{
    private int valor;
    private int chave;

    public MoedaSegura(int valorInicial)
    {
        chave = Random.Range(int.MinValue, int.MaxValue);
        valor = valorInicial ^ chave;
    }

    public int Valor
    {
        get => valor ^ chave;
        set => valor = value ^ chave;
    }
}
