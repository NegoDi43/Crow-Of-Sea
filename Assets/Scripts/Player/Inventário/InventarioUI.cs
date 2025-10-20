using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public GameObject painelInventario; // arraste o painel no Inspector
    public GameObject[] butoes; // bot�es de adicionar e remover
    public Inventario inventario; // refer�ncia ao invent�rio
    public GameObject slotPrefab; // prefab do slot de invent�rio
    public Transform containerSlots; // container onde os slots ser�o instanciados
    private bool inventarioAtivo = false; // estado do invent�rio

    private List<SlotUI> slotsUI = new List<SlotUI>();

    void Start()
    {
        painelInventario.SetActive(false); // inicia com o invent�rio desativado

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

    // M�todo para chamar no bot�o
    public void AlternarInventario()
    {
        inventarioAtivo = !inventarioAtivo;               // inverte o estado
        painelInventario.SetActive(inventarioAtivo);     // ativa/desativa painel

        if (inventarioAtivo) // se invent�rio ativo
        {
            foreach (GameObject botao in butoes)
            {
                botao.SetActive(true);
            }
        }
        else if (!inventarioAtivo) // se invent�rio n�o ativo
        {
            foreach (GameObject botao in butoes)
            {
                botao.SetActive(false);
            }
        }
    }
}
