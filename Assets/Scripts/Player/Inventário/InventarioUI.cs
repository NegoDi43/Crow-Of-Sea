using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventarioUI : MonoBehaviour
{
    public Transform conteudo; // Local onde os ícones vão aparecer
    public GameObject prefabSlot; // Prefab de cada slot visual

    public void Atualizar(Inventario inventario)
    {
        // Limpa os slots antigos
        foreach (Transform filho in conteudo)
        {
            Destroy(filho.gameObject);
        }

        // Cria novos slots
        foreach (SlotInventario slot in inventario.slots)
        {
            GameObject novoSlot = Instantiate(prefabSlot, conteudo);
            Image icone = novoSlot.GetComponent<Image>();
            icone.sprite = slot.item.icone;
        }
    }

    public PrefabsItens GetItem(PrefabsItens item)
    {
        return item;
    }
}
