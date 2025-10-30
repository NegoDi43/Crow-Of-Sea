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
    public float tempo = 0;
    public PrefabsItens item;
    public static PrefabsItens referenciaItem;

    void Awake()
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
            quantidadeTexto.text = quantidade >= 1 ? quantidade.ToString() : "0";
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
        tempo = 0;
        if (item != null)
        {
            referenciaItem = item;

            // Aqui você pode abrir descrição, usar item, etc.
            Debug.Log($"?? Clicou no item: {item.nomeItem} (x{quantidadeTexto.text})");
            descricao.text = item.descricao;

            while (tempo < coldownTime)
            {
                tempo += Time.deltaTime;

                if (tempo >= coldownTime)
                {
                    descricao.text = "";
                }
            }

        }
        else
        {
            Debug.Log("Slot vazio.");
            referenciaItem = null;
        }
    }
}
