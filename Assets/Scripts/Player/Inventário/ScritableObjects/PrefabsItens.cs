using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects")]
public abstract class PrefabsItens : ScriptableObject
{
    public enum TipoItem { Arma, Armadura, Pocao, Chave, Recurso, Outro }

    [Header("Valores do item")]
    public string nomeItem;
    public float alcance;
    public TipoItem tipo;
    public Sprite icone;

    [TextArea] public string descricao;
}
