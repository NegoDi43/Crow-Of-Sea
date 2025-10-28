using UnityEngine;
using TMPro;

public class UIEconomia : MonoBehaviour
{
    [SerializeField] private EconomiaSO economia;
    [SerializeField] private GameObject painelCompra;
    [SerializeField] private TextMeshProUGUI textoMoedas;
    [SerializeField] private TextMeshProUGUI textoMoedasPlayer;
    [SerializeField] private int moedasPlayer = 5;

    void Start() // Inicializa a UI com os valores iniciais
    {
        textoMoedasPlayer.text = moedasPlayer.ToString();
        textoMoedas.text = economia.Moedas.ToString();
        economia.DefinirUI(painelCompra);
    }

    private void OnEnable() // Assina o evento quando o objeto é ativado
    {
        economia.OnMoedasMudou += AtualizarTexto;
        AtualizarTexto(economia.Moedas);
    }

    private void OnDisable() // Desassina o evento quando o objeto é desativado
    {
        economia.OnMoedasMudou -= AtualizarTexto;
    }

    void AtualizarTexto(int valor) // Atualiza o texto das moedas na UI
    {
        textoMoedas.text = valor.ToString();
    }

    public void ComprarItem(int preco, Inventario inventario) // Lógica de compra do item
    {
        if (economia.item.comprado == false)
        {
            if (moedasPlayer >= economia.Moedas)
            {
                moedasPlayer -= economia.Moedas;
                textoMoedasPlayer.text = moedasPlayer.ToString();
                inventario.AdicionarItem(economia.item, 1);
                Debug.Log($"Compra realizada!\nX{moedasPlayer}");
                economia.item.comprado = true;
                return;
            }
            else
            {
                Debug.Log("Moedas insuficientes!");
                return;
            }
        }
        else
        {
            Debug.Log("Item já comprado!");
            return;
        }
    }
}
