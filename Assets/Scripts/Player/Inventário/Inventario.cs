using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    public PrefabsItens itemTeste; // Item de teste para adicionar ao inventário
    public InventarioUI inventarioUI;
    public int tamanhoMaximo = 20;

    void Start()
    {
        for (int i = 0; i < tamanhoMaximo; i++)
        {
            AdicionarItem(itemTeste, 5);
            RemoverItem(itemTeste, 1);
            inventarioUI.AtualizarUI();
        }
    }

    public void AdicionarItem(PrefabsItens novoItem, int quantidade = 1)
    {
        // 1️⃣ Verifica se já existe o mesmo item no inventário
        foreach (SlotInventario slot in slots)
        {
            if (slot.item == novoItem)
            {
                slots.Add(new SlotInventario(novoItem, quantidade));

                slot.quantidade += quantidade;
                inventarioUI.AtualizarUI();
                return;
            }
        }

        // 2️⃣ Se não existe, cria um novo slot
        if (slots.Count < tamanhoMaximo)
        {
            slots.Add(new SlotInventario(novoItem, quantidade));
            Debug.Log($"Novo item adicionado: {novoItem.nomeItem} x{quantidade}");
            inventarioUI.AtualizarUI();
        }
        else
        {
            Debug.Log("Inventário cheio!");
            inventarioUI.AtualizarUI();
        }
    }

    // 🧠 Remover item
    public void RemoverItem(PrefabsItens itemRemover, int quantidade = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == itemRemover)
            {
                slots[i].quantidade -= quantidade;
                slots.Remove(new SlotInventario(itemRemover, quantidade));
                inventarioUI.AtualizarUI();

                // Se quantidade <= 0, remove o slot
                if (slots[i].quantidade <= 0)
                {
                    Debug.Log($"Item {itemRemover.nomeItem} removido completamente.");
                    inventarioUI.AtualizarUI();
                }
                else
                {
                    Debug.Log($"Removido {quantidade}x {itemRemover.nomeItem} (restam: {slots[i].quantidade})");
                    inventarioUI.AtualizarUI();
                }
                return;
            }
        }

        Debug.Log("Item não encontrado no inventário.");
    }

    // 🧠 Verificar se tem um item
    public bool TemItem(PrefabsItens item)
    {
        foreach (SlotInventario slot in slots)
        {
            if (slot.item == item)
                return true;
        }
        return false;
    }

    // 🧠 Mostrar o conteúdo no console (debug)
    public void MostrarInventario()
    {
        Debug.Log("📦 Inventário Atual:");
        foreach (SlotInventario slot in slots)
        {
            Debug.Log($"{slot.item.nomeItem} x{slot.quantidade}");
        }
    }
}
