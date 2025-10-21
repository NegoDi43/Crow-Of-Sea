using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public GameObject painelInventario; // arraste o painel no Inspector
    public GameObject[] butoes; // botões de adicionar e remover
    public Inventario inventario; // referência ao inventário
    public GameObject slotPrefab; // prefab do slot de inventário
    public Transform containerSlots; // container onde os slots serão instanciados
    private bool inventarioAtivo = false; // estado do inventário

    private List<SlotUI> slotsUI = new List<SlotUI>();

    void Start()
    {
        painelInventario.SetActive(false); // inicia com o inventário desativado

        if (butoes == null || butoes.Length == 0)
        {
            butoes[0] = GameObject.Find("Adicionar").GetComponent<GameObject>();
            butoes[1] = GameObject.Find("Remover").GetComponent<GameObject>();
        }

        foreach (GameObject botao in butoes)
        {
            botao.SetActive(false);
        }
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

    // Método para chamar no botão
    public void AlternarInventario()
    {
        inventarioAtivo = !inventarioAtivo;               // inverte o estado
        painelInventario.SetActive(inventarioAtivo);     // ativa/desativa painel

        if (inventarioAtivo) // se inventário ativo
        {
            foreach (GameObject botao in butoes)
            {
                botao.SetActive(true);
            }
        }
        else if (!inventarioAtivo) // se inventário não ativo
        {
            foreach (GameObject botao in butoes)
            {
                botao.SetActive(false);
            }
        }
    }
}
