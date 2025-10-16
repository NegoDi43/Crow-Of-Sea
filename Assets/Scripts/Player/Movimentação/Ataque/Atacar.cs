using System.Collections;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    [SerializeField] private Status statusPlayer;
    [SerializeField] private GameObject cortePrefab;
    [SerializeField] private Transform posicaoCorte;
    [SerializeField] private float tempoEntreCortes = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusPlayer = GameObject.FindAnyObjectByType<Status>().gameObject.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ataque()
    {
       StartCoroutine(AtacarCorrotina());

    }

    IEnumerator AtacarCorrotina()
    {
        Instantiate(cortePrefab, posicaoCorte.position, posicaoCorte.rotation);
        yield return new WaitForSeconds(tempoEntreCortes);
    }
}
