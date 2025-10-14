using UnityEngine;

[CreateAssetMenu(menuName = "New Item/ArmaduraSO")]
public class ArmaduraSO : PrefabsItens
{
    public int defesa;
    TipoItem tipoItem = TipoItem.Armadura;
    [TextArea] public string descricao;
}
