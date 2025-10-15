using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image icone;
    public TextMeshProUGUI quantidadeTexto;
    public TextMeshProUGUI descricao;
    public float coldownTime = 4f; // Tempo de cooldown em segundos

    private PrefabsItens item;
    
    void Start()
    {
        if (descricao == null)
        {
            descricao = GameObject.Find("TextoDescricao").GetComponent<TextMeshProUGUI>();
        }
        descricao.text = "";
    }

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
            // Aqui voc� pode abrir descri��o, usar item, etc.
            Debug.Log($"?? Clicou no item: {item.nomeItem} (x{quantidadeTexto})");
            descricao.text = item.descricao;
        }
        else
        {
            Debug.Log("Slot vazio.");
        }
    }
}
