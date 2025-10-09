using UnityEngine;

public class PegaReferencia : MonoBehaviour
{
    public PrefabsItens referencia;
    public string nome;
    public int dano;
    public float alcance;
    public int cura;
    public int defesa;
    public Sprite icone;
    public int numeroEspada;

    void Awake()
    {
        nome = referencia.nome;
        dano = referencia.dano;
        alcance = referencia.alcance;
        cura = referencia.cura;
        defesa = referencia.defesa;
        icone = referencia.icone;
        numeroEspada = referencia.numeroEspada;

        GetComponent<SpriteRenderer>().sprite = icone;
    }
}
