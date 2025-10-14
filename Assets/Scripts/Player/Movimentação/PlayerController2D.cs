using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    public VirtualJoystick2D joystick;
    [SerializeField] private Status statusPlayer;

    [SerializeField] private bool correndo = false;

    private Rigidbody2D rb;

    void Start()
    {
        statusPlayer = GameObject.FindAnyObjectByType<Status>().gameObject.GetComponent<Status>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 input = joystick != null ? joystick.InputDirection : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = input * statusPlayer.GetVelocidade();
    }

    public void Correr()
    {         
        if (statusPlayer.GetStaminaAtual() > 0)
        {
            correndo = true;
            statusPlayer.Correndo();
        }
        
    }

    public bool Correndo(bool v)
    {
        v = correndo;
        return correndo;
    }
        
}
