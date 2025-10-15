using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image icone;
    public TextMeshProUGUI quantidadeTexto;
    public TextMeshProUGUI descricao;

    private PrefabsItens item;

    public void AtualizarSlot(PrefabsItens novoItem, int quantidade)
    {
        item = novoItem;

        if (item != null)
        {
            icone.sprite = item.icone;
            icone.enabled = true;
            quantidadeTexto.text = quantidade > 1 ? quantidade.ToString() : "";
        }
        else
        {
            icone.sprite = null;
            icone.enabled = false;
            quantidadeTexto.text = "";
        }
    }

    // Detecta o clique/tap
    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Debug.Log($"?? Clicou no item: {item.nomeItem} (x{quantidadeTexto})");
            descricao.text = item.descricao;
        }
        else
        {
            Debug.Log("Slot vazio.");
        }
    }

    //public void ClicarNoSlot()
    //{
    //    if (item != null)
    //    {
    //        Debug.Log("Clicou em: " + item.nomeItem);

    //        // Aqui você pode abrir descrição, usar item, etc.
    //        Debug.Log($"Item: {item.nomeItem}\nTipo: {item.tipo}\nAlcance: {item.alcance}");
    //    }
    //}
}
