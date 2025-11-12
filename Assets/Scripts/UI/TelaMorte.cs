using UnityEngine;

public class TelaMorte : MonoBehaviour
{
    [SerializeField] private GameObject telaMorte;

    void Start()
    {
        telaMorte.GetComponent<GameObject>();
        telaMorte.SetActive(false);

        if (telaMorte == null)
        {
            telaMorte = GameObject.FindGameObjectWithTag("TelaMorte");
            telaMorte.GetComponent<GameObject>();
            Debug.LogError("Tela de morte não está atribuída no inspetor.");
        }
    }

    private void Morrer()
    {
        if (Status.status.Vida <= 0)
        {
            telaMorte.SetActive(true);
            //Time.timeScale = 0f;
        }
    }
}
