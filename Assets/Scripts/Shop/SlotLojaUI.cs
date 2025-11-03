using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SlotLojaUI : MonoBehaviour, IPointerClickHandler // Classe para gerenciar o slot da loja na UI
{
    [Header("Não precisa colocar essas referências!")]
    public PrefabsItens item; // Item associado a este slot
    [Header("Componentes UI")]
    public Image icone;
    public TextMeshProUGUI TextoPreco;
    public TextMeshProUGUI descricao;
    public float coldownTime = 4f; // Tempo de cooldown em segundos
    public float tempo = 0;
    public static PrefabsItens referenciaItem; // Referência estática ao item selecionado

    void Awake()
    {
        if (descricao == null)
        {
            descricao = GameObject.Find("TextoDescricao").GetComponent<TextMeshProUGUI>();
        }
        descricao.text = "";
    }

    public void AtualizarSlot(PrefabsItens novoItem, int preco)
    {
        item = novoItem;
        referenciaItem = item;

        if (item != null)
        {
            icone.sprite = item.icone;
            icone.enabled = true;
            TextoPreco.text = preco.ToString();
        }
        else
        {
            icone.sprite = null;
            icone.enabled = false;
            TextoPreco.text = "";
        }
    }

    // Detecta o clique/tap
    public void OnPointerClick(PointerEventData eventData)
    {
        referenciaItem = item;
        tempo = 0;
        if (item != null)
        {
            item.comprado = false; // Reseta o estado de compra do item
            referenciaItem = item;

            // Aqui você pode abrir descrição, usar item, etc.
            Debug.Log($"?? Clicou no item: {item.nomeItem} (x{TextoPreco.text})");
            descricao.text = item.descricao;

            while (tempo < coldownTime)
            {
                tempo += Time.deltaTime;

                if (tempo >= coldownTime)
                {
                    StartCoroutine(TempoDescricao());
                }
            }
        }
        else
        {
            Debug.Log("Slot vazio.");
            referenciaItem = item;
        }
    }

    IEnumerator TempoDescricao()
    {
        yield return new WaitForSeconds(coldownTime);
        descricao.text = "";
    }
}
