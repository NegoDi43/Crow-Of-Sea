using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaUI : MonoBehaviour
{
    [SerializeField] private GameObject painelLoja;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject butaoNpc;
    [SerializeField] private Transform conteudo;
    public Loja loja; // Referência à classe Loja

    void Start()
    {
        painelLoja.SetActive(false);
    }

    public void AlternarLoja() // Ativa/Desativa o painel da loja
    {
        bool estaAtivo = painelLoja.activeSelf;
        painelLoja.SetActive(!estaAtivo);

        if (!estaAtivo) // Se a loja foi ativada
        {
            foreach (var slot in loja.slotsLoja)
            {
                GameObject novoSlot = Instantiate(slotPrefab, conteudo);
                novoSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.Moedas.ToString();
                novoSlot.GetComponent<Image>().sprite = slot.item.icone;

            }
            butaoNpc.SetActive(true);
        }
        else // Se a loja foi desativada
        {
            foreach (Transform filho in conteudo)
            {
                Destroy(filho.gameObject);
            }
            butaoNpc.SetActive(true);
        }
    }
}
