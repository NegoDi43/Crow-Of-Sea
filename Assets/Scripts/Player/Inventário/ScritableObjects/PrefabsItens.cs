using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ScriptableObjects")]
public abstract class PrefabsItens : ScriptableObject
{
    public enum TipoItem { Arma, Armadura, Pocao, Chave, Recurso, Outro }

    [Header("Valores do item")]
    public string nomeItem;
    public float alcance;
    public TipoItem tipo;
    public Sprite icone = null;
    [Header("Preço")]
    [SerializeField] private int moedas; // Quantidade de moedas
    public event Action<int> OnMoedasMudou; // Evento para notificar mudanças nas moedas
    public int Moedas => moedas; // Propriedade somente leitura para acessar as moedas
    public bool comprado; // <-- controla se já foi comprado
    [Header("Descrição")]
    [TextArea] public string descricao;

    public void AdicionarMoedas(int quantidade)
    {
        moedas += quantidade;
        OnMoedasMudou?.Invoke(moedas);
    }

    public bool GastarMoedas(int quantidade)
    {
        if (moedas < quantidade)
            return false;
        Debug.Log("GastarMoedas: " + moedas + " - " + quantidade);

        moedas -= quantidade;
        OnMoedasMudou?.Invoke(moedas);
        return true;
    }

    public void DefinirUI(GameObject ui) // Recebe o slot de compra
    {
        ui.GetComponent<Image>().sprite = icone;
    }
}
