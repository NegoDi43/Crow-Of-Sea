using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public Inventario inventario;
    public GameObject slotPrefab;
    public Transform containerSlots;

    private List<SlotUI> slotsUI = new List<SlotUI>();

    void Awake()
    {

    }
    void Start()
    {
        AtualizarUI();
    }

    public void AtualizarUI()
    {
        // Limpa slots antigos
        foreach (Transform filho in containerSlots)
        Destroy(filho.gameObject);

        slotsUI.Clear();

        // Cria novos slots
        foreach (var slot in inventario.slots)
        {
            GameObject novoSlot = Instantiate(slotPrefab, containerSlots);
            SlotUI slotUI = novoSlot.GetComponent<SlotUI>();
            slotUI.AtualizarSlot(slot.item, slot.quantidade);
            slotsUI.Add(slotUI);
        }
    }
}
