using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class LojaUI : MonoBehaviour // Gerencia a interface da loja
{
    [Header("Componentes da UI")]
    [SerializeField] private GameObject painelLoja; // Referência ao painel da loja
    [SerializeField] private GameObject slotPrefab; // Prefab do slot da loja
    [SerializeField] private GameObject butaoNpc; // Botão do NPC
    [SerializeField] private Transform conteudo; // Conteúdo onde os slots serão instanciados
    //public Loja loja; // Referência à classe Loja
    [Header("Não precisa colocar essas referências!")]
    private List<SlotLojaUI> slotsLojaUI = new List<SlotLojaUI>(); // lista para armazenar os slots UI

    void Start()
    {
        painelLoja.SetActive(false);
    }

    //public void AlternarLoja() // Ativa/Desativa o painel da loja
    //{
    //    bool estaAtivo = painelLoja.activeSelf;
    //    painelLoja.SetActive(!estaAtivo);

    //    if (!estaAtivo) // Se a loja foi ativada
    //    {
    //        foreach (Transform filho in conteudo)
    //            Destroy(filho.gameObject);

    //        slotsLojaUI.Clear();
    //        foreach (var slot in loja.slotsLoja)
    //        {
    //            GameObject novoSlot = Instantiate(slotPrefab, conteudo); // Cria um novo slot na UI
    //            novoSlot.GetComponent<Image>().sprite = slot.item.icone; // Define o fundo do slot
    //            SlotLojaUI slotLojaUI = novoSlot.GetComponent<SlotLojaUI>();
    //            slotLojaUI.AtualizarSlot(slot.item, slot.item.Moedas);
    //            slotsLojaUI.Add(slotLojaUI);
    //        }
    //        butaoNpc.SetActive(true);
    //    }
    //    else // Se a loja foi desativada
    //    {
    //        foreach (Transform filho in conteudo)
    //        {
    //            Destroy(filho.gameObject);
    //            slotsLojaUI.Clear();
    //        }
    //        butaoNpc.SetActive(true);
    //    }
    //}
}
