using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private VirtualJoystick2D joystick;
    [SerializeField] private Status statusPlayer;

    [SerializeField] private bool correndo = false;

    private Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        statusPlayer = GameObject.FindAnyObjectByType<Status>().gameObject.GetComponent<Status>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 input = joystick != null ? joystick.InputDirection : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = input * statusPlayer.GetVelocidade();
        
    }

    public void AnimaAndar()
    {
        animator.SetTrigger("Andar");
        animator.SetBool("Andando", false);
    }
    public void AnimaParar()
    {
        animator.SetTrigger("Parar");
        animator.SetBool("Andando", true);
    }

    public void AnimaAtacarCorte()
    {
        animator.SetTrigger("Atacar");
    }

    public void AnimaMorrer()
    {
        animator.SetTrigger("Morrer");
    }

    public void AnimaDano()
    {
        animator.SetTrigger("Dano");
    }
}
