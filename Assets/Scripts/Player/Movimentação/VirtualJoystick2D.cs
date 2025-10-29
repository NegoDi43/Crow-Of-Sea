using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick2D : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background; // fundo do joystick
    public RectTransform handle;     // bolinha
    public float handleLimit = 0.6f; // limite do movimento do handle (0..1)
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private PlayerController2D playerController;
    private GameObject player;

    private Vector2 input = Vector2.zero;
    private Vector2 backgroundSize;


    void Start()
    {
        backgroundSize = background.sizeDelta; // pega o tamanho do fundo
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
        ResetHandle();
    }

    void Update()
    {
        Rotacionar();
    }   

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData); // chama OnDrag ao pressionar

    public void OnDrag(PointerEventData eventData) // enquanto arrasta
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            x = localPoint.x / (backgroundSize.x * 0.5f);
            y = localPoint.y / (backgroundSize.y * 0.5f);
            Vector2 raw = new Vector2(x, y);

            // clamp dentro do círculo
            input = raw.magnitude > 1 ? raw.normalized : raw;

            // move a bolinha
            handle.anchoredPosition = input * (backgroundSize * 0.5f * handleLimit);
            playerController.AnimaAndar();
        }
    }

    public void OnPointerUp(PointerEventData eventData) // ao soltar
    {
        input = Vector2.zero;
        ResetHandle();

    }

    void ResetHandle() // reseta a posição da bolinha
    {
        handle.anchoredPosition = Vector2.zero;
        playerController.AnimaParar();
    }

    private void Rotacionar()
    {
        if (x > 0)
        {
            // Corrigido: acessar o SpriteRenderer do player para definir flipX
            var spriteRenderer = playerController.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (x < 0)
        {
            var spriteRenderer = playerController.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
    // acesso ao input
    public Vector2 InputDirection => input;
    public float Horizontal => input.x;
    public float Vertical => input.y;

    public float RetornaX()
    {
        return x;
    }
    public float RetornaY()
    {
        return y;
    }

}
