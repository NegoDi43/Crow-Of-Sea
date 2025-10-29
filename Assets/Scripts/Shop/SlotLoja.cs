using UnityEngine;

[System.Serializable]
public class SlotLoja
{
    public PrefabsItens item; // Referência ao prefab do item

    public SlotLoja (PrefabsItens item)
    {
        this.item = item;
    }
}
