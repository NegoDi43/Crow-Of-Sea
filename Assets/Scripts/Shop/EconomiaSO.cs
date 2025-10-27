using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Economia", menuName = "Sistema/Economia")]
public class EconomiaSO : ScriptableObject
{
    [SerializeField] private int moedas;
    [SerializeField] private PrefabsItens item;


    public event Action<int> OnMoedasMudou;

    public int Moedas => moedas;

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




}
