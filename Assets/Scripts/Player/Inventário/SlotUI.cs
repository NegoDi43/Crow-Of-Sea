using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image icone;
    public TextMeshProUGUI quantidadeTexto;
    public TextMeshProUGUI descricao;
    public GameObject[] butoes;
    public float coldownTime = 4f; // Tempo de cooldown em segundos
    private PrefabsItens item;
    public static PrefabsItens ReferenciaItem;

    void Start()
    {
        if (descricao == null)
        {
            descricao = GameObject.Find("TextoDescricao").GetComponent<TextMeshProUGUI>();
        }

        if (butoes == null || butoes.Length == 0)
        {
            butoes[0] = GameObject.Find("Adicionar").GetComponent<GameObject>();
            butoes[1] = GameObject.Find("Remover").GetComponent<GameObject>();
        }

        foreach (GameObject botao in butoes)
        {
            botao.SetActive(false);
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
        float tempo = 0;

        if (item != null)
        {
            ReferenciaItem = item;

            // Aqui você pode abrir descrição, usar item, etc.
            Debug.Log($"?? Clicou no item: {item.nomeItem} (x{quantidadeTexto})");
            descricao.text = item.descricao;

            foreach (GameObject botao in butoes)
            {
                botao.SetActive(true);
            }

            while (tempo < coldownTime)
            {
                tempo += Time.deltaTime;
                if (tempo >= coldownTime)
                {
                    foreach (GameObject botao in butoes)
                    {
                        botao.SetActive(false);
                    }
                    descricao.text = "";
                }
            }

        }
        else
        {
            Debug.Log("Slot vazio.");
            ReferenciaItem = null;
        }
    }
}
