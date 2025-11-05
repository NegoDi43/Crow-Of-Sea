using UnityEngine;

public class ItemInteraction2D : MonoBehaviour
{
    [Header("Informações do Item")]
    public string itemNome;
    [TextArea(2, 5)]
    public string itemDescricao;

    private bool jogadorPerto = false;

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            jogadorPerto = true;
            UIItemInfo2D.instancia.MostrarInfo(this);
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
            Debug.Log("Item pego: " + itemNome);
            UIItemInfo2D.instancia.EsconderInfo();
            Destroy(gameObject);
        }
    }
}