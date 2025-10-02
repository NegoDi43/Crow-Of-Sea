using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsItens", menuName = "Scriptable Objects/New Item")]
public class PrefabsItens : ScriptableObject
{
    public string nome;
    public int dano;
    public float alcance;
    public int cura;
    public int defesa;
    public Sprite icone;
    public int numeroEspada;
}
