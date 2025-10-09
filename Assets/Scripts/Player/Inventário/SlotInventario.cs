using UnityEngine;

[System.Serializable]
public class SlotInventario : MonoBehaviour
{
    public PrefabsItens item;
    public int quantidade = 0;

    public SlotInventario (PrefabsItens item, int quantidade)
    {
        this.item = item;

        if (item.tipo == PrefabsItens.TipoItem.Pocao)
            this.quantidade = quantidade;
    }
}

