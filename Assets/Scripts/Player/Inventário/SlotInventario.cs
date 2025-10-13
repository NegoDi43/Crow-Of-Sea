using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class SlotInventario : MonoBehaviour
{
    Transform conteudo; // Local onde os ícones vão aparecer
    public PrefabsItens item; // Referência ao item no slot
    public int quantidade = 0; // Quantidade do item no slot
    public GameObject itemIcone; // Ícone do item no slot

    // Construtor para inicializar o slot com um item e quantidade

    public GameObject Background(GameObject backgroundPrefab, SlotInventario slot)
    {
        GameObject novoSlot = Instantiate(backgroundPrefab, conteudo);
        return novoSlot;
    }

    public SlotInventario(PrefabsItens item, int quantidade)
    {
        this.item = item;
        this.quantidade = quantidade;
    }

    public SlotInventario()
    {
        this.item = null;
        this.quantidade = 0;
    }
}

