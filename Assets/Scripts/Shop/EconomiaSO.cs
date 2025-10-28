using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Economia", menuName = "Sistema/Economia")]
public class EconomiaSO : ScriptableObject
{
    [SerializeField] private int moedas;
    public PrefabsItens item; // Referência ao item associado à economia

    public event Action<int> OnMoedasMudou; // Evento para notificar mudanças nas moedas

    public int Moedas => moedas; // Propriedade somente leitura para acessar as moedas

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
        ui.GetComponent<Image>().sprite = item.icone;
    }




}
