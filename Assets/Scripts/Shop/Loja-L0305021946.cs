using UnityEngine;

public class Loja : MonoBehaviour
{
    [SerializeField] private EconomiaSO economia;

    public void ComprarItem(int preco)
    {
        if (economia.GastarMoedas(preco))
            Debug.Log("Compra realizada!");
        else
            Debug.Log("Moedas insuficientes!");
    }
}
