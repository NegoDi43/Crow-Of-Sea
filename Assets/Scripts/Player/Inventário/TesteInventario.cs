using UnityEngine;
using static PrefabsItens;

public class TesteInventario : MonoBehaviour
{
        public Inventario inventario;
        public PrefabsItens espada;

        void Start()
        {
            inventario.AdicionarItem(espada);
        }
}
