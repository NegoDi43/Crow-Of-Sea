using UnityEngine;
using TMPro;

public class UIEconomia : MonoBehaviour
{
    [SerializeField] private EconomiaSO economia;
    [SerializeField] private GameObject painelCompra;
    [SerializeField] private TextMeshProUGUI textoMoedas;
    [SerializeField] private TextMeshProUGUI textoMoedasPlayer;
    [SerializeField] private int moedasPlayer = 5;

    void Start()
    {
        textoMoedasPlayer.text = moedasPlayer.ToString();
        textoMoedas.text = economia.Moedas.ToString();
    }

    private void OnEnable()
    {
        economia.OnMoedasMudou += AtualizarTexto;
        AtualizarTexto(economia.Moedas);
    }

    private void OnDisable()
    {
        economia.OnMoedasMudou -= AtualizarTexto;
    }

    void AtualizarTexto(int valor)
    {
        textoMoedas.text = valor.ToString();
    }

    public void ComprarItem(int preco)
    {
        if (moedasPlayer >= economia.Moedas)
        {
            moedasPlayer -= economia.Moedas;
            textoMoedasPlayer.text = moedasPlayer.ToString();
            Debug.Log($"Compra realizada!\nX{moedasPlayer}");
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }
}
