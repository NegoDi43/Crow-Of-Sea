using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    public InventarioUI uiInventario;
    public int tamanhoMaximo = 20;

    // Adicionar item ao inventário
    public void AdicionarItem(PrefabsItens item)
    {
        // Adiciona novo item
        slots.Add(new SlotInventario(item, 1));
        AtualizarUI();

        // Se for uma poção, aumenta a quantidade
        if (item.tipo == PrefabsItens.TipoItem.Pocao)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item == item)
                {
                    slots[i].quantidade++;
                    AtualizarUI();
                    uiInventario.GetItem(item);
                    return;
                }
            }
        }
    }

    // Remover item
    public void RemoverItem(PrefabsItens item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots.RemoveAt(i);
                AtualizarUI();
                uiInventario.GetItem(item);

                if (item.tipo == PrefabsItens.TipoItem.Pocao)
                {
                    slots[i].quantidade--;
                        if (slots[i].quantidade <= 0)
                        {
                            slots.RemoveAt(i);
                        }
                }
            }
        }
    }

    // Atualiza a interface
    public void AtualizarUI()
    {
        if (uiInventario != null)
            uiInventario.Atualizar(this);
    }
}

