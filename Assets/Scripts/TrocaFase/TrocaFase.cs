using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrocaFase : MonoBehaviour
{
    [SerializeField] private string nomeFase;

    IEnumerator TrocarFase(float tempo, string nomeFase)
    {
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene(nomeFase); // substitua pelo nome da próxima fase
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barco") || other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TrocarFase(1f, nomeFase)); // espera 1 segundo antes de trocar de fase
        }
    }
}
