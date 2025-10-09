using TMPro;
using UnityEngine;

public class GanhodeXp : MonoBehaviour
{
    [Header("Xp")]
    [SerializeField] private int xpPorInimigo = 5;
    [SerializeField] private int xpPorMissao = 50;
    [SerializeField] private int xpNecessarioParaNivelUp = 10;
    [SerializeField] private int xpAtual = 0;

    [SerializeField] private int levellMaximo = 100;
    [SerializeField] private int levelAtual = 0;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Status")]
    [SerializeField] private Status status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        UparNivel();
    }

    public void FixedUpdate()
    {
        levelText.text = levelAtual.ToString();
    }
    private void UparNivel()
    {
        if (xpAtual == xpNecessarioParaNivelUp)
        {
            levelAtual++;
            xpNecessarioParaNivelUp += 15;
            xpAtual = 0;
            status.GanharPontos();
        }
    }

    //Getters e Setters
    public int GetXpPorInimigo()
    {
        return xpPorInimigo;
    }
    public int GetXpPorMissao()
    {
        return xpPorMissao;
    }
    public int GetXpNecessarioParaNivelUp()
    {
        return xpNecessarioParaNivelUp;
    }
    public int GetXpAtual()
    {
        return xpAtual;
    }

}
