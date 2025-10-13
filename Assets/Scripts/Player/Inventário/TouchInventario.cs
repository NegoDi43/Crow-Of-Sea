using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TouchInventario : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    public GameObject butaoAdicionar;
    public GameObject butaoRemover;
    //public GameObject statusPanel; // Painel que mostra os status
    //public Text nameText;
    //public Text healthText;
    //public Text attackText;
    //public Text defenseText;

    void Start()
    {
        butaoAdicionar.SetActive(false);
        butaoRemover.SetActive(false);

        if (butaoAdicionar == null)
        {
            butaoAdicionar = GameObject.Find("AdicionarItem");
        }
        if (butaoRemover == null)
        {
            butaoRemover = GameObject.Find("RemoverItem");
        }
    }

    void Update()
    {

    }

    //// =============================
    //// FUNÇÃO PARA MOSTRAR STATUS
    //// =============================
    //void ShowStatus(GameObject touchedObject)
    //{
    //    PrefabsItens item = touchedObject.GetComponent<PrefabsItens>();
    //    if (item != null)
    //    {
    //        statusPanel.SetActive(true);
    //    }
    //}
    
    public PrefabsItens  GetItem(PrefabsItens item)
    {
        return item;
    }
}
