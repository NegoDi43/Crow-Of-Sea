using TMPro;
using UnityEngine;

public class StatusUi : MonoBehaviour
{
    [Header("Status UI")]

    [SerializeField] private GameObject Painel;
    [SerializeField] private TextMeshProUGUI textVida;
    [SerializeField] private TextMeshProUGUI textDano;
    [SerializeField] private TextMeshProUGUI textVelocidade;
    [SerializeField] private TextMeshProUGUI textStamina;
    
    [SerializeField] private TextMeshProUGUI textPontos;


    [Header("Refêrencias")]
    [SerializeField] private Status status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        textVida.text = status.GetPontosVida().ToString("");
        textStamina.text = status.GetPontosStamina().ToString("");
        textDano.text = status.GetPontosDano().ToString("");
        textVelocidade.text = status.GetPontosVelocidade().ToString("");
        textPontos.text = status.GetPontos().ToString("");
    }

    public void OpenPainel()
    {
        Painel.SetActive(true);
    }

    public void ClosePainel()
    {
        Painel.SetActive(false);
    }

    public void UparVida()
    {
        status.UpVidaMaxima();
    }
    public void UparSpeed()
    {
        status.UpVelocidade();
    }
    public void UparDano()
    {
        status.UpDanoMaximo();
    }
    public void UparStamina()
    {
        status.UpStamina();
    }
}

