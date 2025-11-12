using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour

{
[SerializeField] private string NomeDaFase;

    public void IniciarJogo()
    {
       SceneManager.LoadScene(NomeDaFase);
    }
}
