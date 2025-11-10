using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemInfo2D : MonoBehaviour
{
    public static UIItemInfo2D instancia;
    public ItemInteraction2D[] itemInteraction;

    [Header("Referências do Painel")]
    public GameObject painel;
    public TextMeshProUGUI nomeTexto;
    public TextMeshProUGUI descricaoTexto;
    public Button botaoPegar;

    private PrefabsItens itemAtual;

    void Awake()
    {
        instancia = this;
        painel.SetActive(false);
    }

    public void MostrarInfo(PrefabsItens item)
    {
        itemAtual = item;
        nomeTexto.text = item.nomeItem;
        descricaoTexto.text = item.descricao;
        painel.SetActive(true);

        foreach (var itemInteraction in itemInteraction)
            if (itemInteraction.item == itemAtual)
            {
                botaoPegar.onClick.RemoveAllListeners();
                botaoPegar.onClick.AddListener(() => itemInteraction.PegarItem());
            }
    }

    public void EsconderInfo()
    {
        painel.SetActive(false);
        itemAtual = null;
    }
}