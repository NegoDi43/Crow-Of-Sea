using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider barraDeVida;
    [SerializeField] private Slider barraDeStamina;
    [SerializeField] private Slider barraDeXP;

    [Header("Refêrencias")]
    [SerializeField] private Status status;
    [SerializeField] private GanhodeXp xp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barraDeVida = GameObject.FindGameObjectWithTag("SliderVida").GetComponent<Slider>();
        barraDeStamina = GameObject.FindGameObjectWithTag("SlliderStamina").GetComponent<Slider>();
        barraDeXP = GameObject.FindGameObjectWithTag("SliderXP").GetComponent<Slider>();

        xp = GameObject.FindGameObjectWithTag("Player").GetComponent<GanhodeXp>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        AtualizaVida();
        AtualizaStamina();
        AtualizaXP();
    }

    private void AtualizaVida()
    {
        barraDeVida.maxValue = status.GetVidaMaxima();
        barraDeVida.value = status.GetVidaAtual();
    }

    private void AtualizaStamina()
    {
        barraDeStamina.maxValue = status.GetStaminaMax();
        barraDeStamina.value = status.GetStaminaAtual();
    }

    private void AtualizaXP()
    {
        barraDeXP.maxValue = xp.GetXpNecessarioParaNivelUp();
        barraDeXP.value = xp.GetXpAtual();
    }
}
