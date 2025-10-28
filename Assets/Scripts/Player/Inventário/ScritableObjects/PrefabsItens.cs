using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects")]
public abstract class PrefabsItens : ScriptableObject
{
    public enum TipoItem { Arma, Armadura, Pocao, Chave, Recurso, Outro }

    [Header("Valores do item")]
    public string nomeItem;
    public float alcance;
    public TipoItem tipo;
    public Sprite icone = null;
    public bool comprado; // <-- controla se já foi comprado
    [TextArea] public string descricao;
}
