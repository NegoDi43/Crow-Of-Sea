using UnityEngine;
using TMPro;

public class UIEconomia : MonoBehaviour
{
    [SerializeField] private EconomiaSO economia;
    [SerializeField] private TextMeshProUGUI textoMoedas;

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
}
