using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    [Header("Painel do Inventário UI")]
    public GameObject painelInventario; // arraste o painel no Inspector
    [Header("Botões de adicionar")]
    public GameObject[] butoes; // botões de adicionar e remover
    [Header("Referência ao Inventário")]
    public Inventario inventario; // referência ao inventário
    [Header("Prefab do Slot e Container")]
    public GameObject slotPrefab; // prefab do slot de inventário
    public Transform containerSlots; // container onde os slots serão instanciados

    private bool inventarioAtivo = false; // estado do inventário

    private List<SlotUI> slotsUI = new List<SlotUI>(); // lista para armazenar os slots UI

    void Start()
    {
        painelInventario.SetActive(false); // inicia com o inventário desativado

        if (butoes == null || butoes.Length == 0)
        {
            butoes[0] = GameObject.Find("Adicionar").GetComponent<GameObject>();
            butoes[1] = GameObject.Find("Remover").GetComponent<GameObject>();
            return;
        }

        foreach (GameObject botao in butoes)
        {
            botao.SetActive(false);
        }
    }
    public void AtualizarUI() // método para atualizar a UI do inventário
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
    public void AlternarInventario() // ativa/desativa o inventário
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
