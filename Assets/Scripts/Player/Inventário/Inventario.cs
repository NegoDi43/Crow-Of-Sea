using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<Item> slots = new List<Item>();
    public InventarioUI uiInventario;
    public int tamanhoMaximo = 20;

    void Start()
    {
        GerraSlots();
    }

    // Adicionar item ao inventário
    public void AdicionarItem(PrefabsItens item)
    {
        // Adiciona novo item
        if (slots.Count < tamanhoMaximo)
        {

        }
        slots.Add(new Item(item, 1));
        //SlotInventario slot = slots[slots.Count - 1];
        AtualizarUI();

        //// Se for uma poção, aumenta a quantidade
        //if (item.tipo == PrefabsItens.TipoItem.Pocao)
        //{
        //    for (int i = 0; i < slots.Count; i++)
        //    {
        //        if (slots[i].item == item)
        //        {
        //            slots[i].quantidade++;
        //            AtualizarUI();
        //            uiInventario.GetItem(item);
        //            return;
        //        }
        //    }
        //}
    }

    public void GerraSlots()
    {
        for (int i = 0; i < tamanhoMaximo; i++)
        {
            slots.Add(new Item (slots[i].item, 1));
            AtualizarUI();
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


    public PrefabsItens GetItem(PrefabsItens item)
    {
        return item;
    }
}

