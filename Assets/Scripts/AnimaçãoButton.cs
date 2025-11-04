using System.Collections;
using UnityEngine;

public class AnimaçãoButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject texto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetTrigger("Abrir");
        StartCoroutine(TempoAbertura());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TempoAbertura()
    {
        yield return new WaitForSeconds(0.5f);
        texto.SetActive(true);
    }

    public void Fechar()
    {
        animator.SetTrigger("Fechar");
        texto.SetActive(false);
    }
}
