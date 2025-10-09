using UnityEngine;

[CreateAssetMenu(menuName = "New Item/ChaveSO")]
public class ChaveSO : PrefabsItens
{
    public string nomeChave;
    public int numeroChave;
    TipoItem tipoItem = TipoItem.Chave;
}
