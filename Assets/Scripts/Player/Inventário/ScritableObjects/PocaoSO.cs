using UnityEngine;

[CreateAssetMenu(menuName = "New Item/PocaoSO")]
public class PocaoSO : PrefabsItens
{
    public enum TipoPocao { Vida, Estamina, Dano, Velocidade }

    public TipoPocao tipoPocao;
    public GameObject efeitoVisual;
    public float duracaoEfeito;
    public int quantidade;

    public void UsarPocao(TipoPocao tipo , int efeito)
    {
        Instantiate(efeitoVisual);
    }
}
