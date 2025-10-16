using System.Collections;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    [SerializeField] private Status statusPlayer;
    [SerializeField] private GameObject cortePrefab;
    [SerializeField] private Transform posicaoCorte;
    [SerializeField] private float tempoEntreCortes = 0.5f;

    [SerializeField] private VirtualJoystick2D joystick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusPlayer = GameObject.FindAnyObjectByType<Status>().gameObject.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        Mira();
    }

    public void Ataque()
    {
       StartCoroutine(AtacarCorrotina());

    }

    IEnumerator AtacarCorrotina()
    {
        Instantiate(cortePrefab, posicaoCorte.position, posicaoCorte.rotation);
        yield return new WaitForSeconds(0.5f);
    }

    public void Mira()
    {
        if (joystick.RetornaX() > 0 && joystick.RetornaX() > joystick.RetornaY())
        {
            posicaoCorte.localPosition = new Vector3(0.3f, 0f, 0f);

            cortePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (joystick.RetornaX() < 0 && joystick.RetornaX() > joystick.RetornaY())
        {
            posicaoCorte.localPosition = new Vector3(-0.3f, 0f, 0f);
            cortePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (joystick.RetornaY() > 0 && joystick.RetornaY() > joystick.RetornaX())
        {
            posicaoCorte.localPosition = new Vector3(0f, 0.3f, 0f);
            posicaoCorte.localRotation = Quaternion.Euler(0f, 0f, 90f);
            cortePrefab.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (joystick.RetornaY() < 0 && joystick.RetornaY() > joystick.RetornaX())
        {
            posicaoCorte.localPosition = new Vector3(0f, -0.3f, 0f);
            posicaoCorte.localRotation = Quaternion.Euler(0f, 0f, 90f);
            cortePrefab.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
