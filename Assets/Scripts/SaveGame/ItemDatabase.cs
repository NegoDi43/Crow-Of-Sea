using UnityEngine;
using System.Collections.Generic;

// Isso permite criar o database como um "Asset" no seu projeto
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventario/Item Database")]
public class ItemDatabase : ScriptableObject
{
    // Arraste TODOS os seus ScriptableObjects de itens (PrefabsItens) para esta lista
    public List<PrefabsItens> todosOsItens;

    // Dicionário para busca rápida
    private Dictionary<string, PrefabsItens> itemLookup;

    // Chamado quando o asset é carregado (ex: ao iniciar o jogo)
    void OnEnable()
    {
        itemLookup = new Dictionary<string, PrefabsItens>();
        foreach (PrefabsItens item in todosOsItens)
        {
            if (item != null && !itemLookup.ContainsKey(item.nomeItem))
            {
                itemLookup.Add(item.nomeItem, item);
            }
        }
    }

    public PrefabsItens GetItemPorID(string id)
    {
        if (itemLookup == null) OnEnable(); // Garante que o dicionário foi inicializado

        if (itemLookup.TryGetValue(id, out PrefabsItens item))
        {
            return item;
        }
        Debug.LogWarning("Item com ID '" + id + "' não encontrado no Database.");
        return null;
    }
}