using UnityEngine;

public class ItemInteraction2D : MonoBehaviour
{
    [Header("Coloque o item aqui!")]
    public PrefabsItens item; // Referência ao item interativo
    private bool jogadorPerto = false;
    public static ItemInteraction2D instancia;
    public Inventario inventario;

    void Awake()
    {
        inventario = FindObjectOfType<Inventario>();    
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            jogadorPerto = true;
            UIItemInfo2D.instancia.MostrarInfo(item);
        }
    }

    void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            jogadorPerto = false;
            UIItemInfo2D.instancia.EsconderInfo();
        }
    }

    public void PegarItem()
    {
        if (jogadorPerto)
        {
            Debug.Log("Item pego: " + item.nomeItem);
            inventario.AdicionarItem(item, 1);
            UIItemInfo2D.instancia.EsconderInfo();
            Destroy(this.gameObject);
        }
    }
}