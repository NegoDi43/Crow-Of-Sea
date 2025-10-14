using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public Image icone;
    public TextMeshProUGUI quantidadeTexto;

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

    public void ClicarNoSlot()
    {
        if (item != null)
        {
            Debug.Log("Clicou em: " + item.nomeItem);
            // Aqui você pode abrir descrição, usar item, etc.
        }
    }
}
