using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public PrefabsItens item; // Referência ao prefab do item
    public int quantidade = 0; // Quantidade do item no slot

    // Construtor para inicializar o slot com um item e quantidade

    public Item(PrefabsItens item, int quantidade)
    {
        this.item = item;
        this.quantidade = quantidade;
    }
}

