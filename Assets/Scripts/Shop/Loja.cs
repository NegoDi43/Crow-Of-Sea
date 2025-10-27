using UnityEngine;
using Unity.Mathematics;
using TMPro;

public class Loja : MonoBehaviour
{
    [SerializeField] private EconomiaSO economia;
    [SerializeField] private TextMeshProUGUI textoMoedasPlayer;
    [SerializeField] private int moedasPlayer = 5;

    void Start()
    {
                    textoMoedasPlayer.text = moedasPlayer.ToString();
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
