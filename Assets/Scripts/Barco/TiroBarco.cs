using UnityEngine;

public class TiroBarco : MonoBehaviour
{
    [SerializeField] private GameObject tiroPrefab;
    [SerializeField] private Transform[] mira;
    [SerializeField] private float velocidadeTiro = 20f;
    [SerializeField] private float tempoDeAtirar = 1f;
    [SerializeField] private float tempoAtual = 0;

    void FixedUpdate()
    {
        tempoAtual += Time.fixedDeltaTime;
    }

    public void InstanciaTiro()
    {
        AtirarBarco(mira[0], - transform.right); // tiro esquerdo
        AtirarBarco(mira[1], transform.right); // tiro direita

        if (tempoAtual >= tempoDeAtirar)
        {
            tempoAtual = 0;
        }
    }

    private void AtirarBarco(Transform pontoTiro, Vector2 direcao)
    {
        if (tempoAtual >= tempoDeAtirar)
        {
            GameObject tiro = Instantiate(tiroPrefab, pontoTiro.position, pontoTiro.rotation);
            Rigidbody2D rbTiro = tiro.GetComponent<Rigidbody2D>();
            rbTiro.linearVelocity = direcao * velocidadeTiro;
            Destroy(tiro, 4f); // destrói o tiro após 5 segundos para evitar acúmulo na cena
        }
    }
}