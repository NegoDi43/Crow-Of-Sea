using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    public PrefabsItens itemAtual = SlotUI.ReferenciaItem; // Item de teste para adicionar ao inventário
    public InventarioUI inventarioUI;
    public int tamanhoMaximo = 5;

    public void AdicionarUI() => AdicionarItem(itemAtual, 1);
    public void RemoverUI() => RemoverItem(itemAtual, 1);

    public void AdicionarItem(PrefabsItens novoItem, int quantidade = 1)
    {
        // 1️⃣ Verifica se já existe o mesmo item no inventário
        foreach (SlotInventario slot in slots)
        {
            if (slot.item == novoItem)
            {
                if (novoItem.tipo == PrefabsItens.TipoItem.Pocao)   //As Poções empilham!
                {
                    // 🔹 Pega a referência da classe filha
                    PocaoSO pocao = novoItem as PocaoSO;

                    switch (pocao.tipoPocao)
                    {
                        case PocaoSO.TipoPocao.Vida:
                            slot.quantidade += quantidade;
                            inventarioUI.AtualizarUI();
                            Debug.Log($"Adicionando poção de vida: {novoItem.nomeItem} x{quantidade}");
                            break;
                        case PocaoSO.TipoPocao.Estamina:    
                            slot.quantidade += quantidade;
                            inventarioUI.AtualizarUI();
                            Debug.Log($"Adicionando poção de estamina: {novoItem.nomeItem} x{quantidade}");
                            break;
                        case PocaoSO.TipoPocao.Dano:
                            slot.quantidade += quantidade;
                            inventarioUI.AtualizarUI();
                            Debug.Log($"Adicionando poção de dano: {novoItem.nomeItem} x{quantidade}");
                            break;
                        default:
                            slot.quantidade += quantidade;
                            inventarioUI.AtualizarUI();
                            Debug.Log($"Adicionando poção de velocidade: {novoItem.nomeItem} x{quantidade}");
                            break;

                    }

                    if (slot.quantidade >= 5)
                    {
                        slot.quantidade = 5; // Limite máximo de poções por slot
                        inventarioUI.AtualizarUI();
                    }
                }
                else
                {
                    slots.Add(new SlotInventario(novoItem, quantidade));
                    inventarioUI.AtualizarUI();
                }

                Debug.Log($"Item existente atualizado: {novoItem.nomeItem} x{slot.quantidade}");
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
        // Procura o slot correspondente ao item
        SlotInventario slot = slots.Find(s => s.item == itemRemover);

        if (slot == null)
        {
            Debug.LogWarning($"Item {itemRemover.nomeItem} não encontrado no inventário.");
            return;
        }

        slot.quantidade -= quantidade;

        if (slot.quantidade <= 0)
        {
            Debug.Log($"Item {itemRemover.nomeItem} removido completamente.");
            slots.Remove(slot);
            inventarioUI.AtualizarUI();
        }
        else
        {
            Debug.Log($"Removido {quantidade}x {itemRemover.nomeItem} (restam: {slot.quantidade})");
            inventarioUI.AtualizarUI();
        }
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
