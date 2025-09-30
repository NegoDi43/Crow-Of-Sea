using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick2D : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background; // fundo do joystick
    public RectTransform handle;     // bolinha
    public float handleLimit = 0.6f; // limite do movimento do handle (0..1)

    private Vector2 input = Vector2.zero;
    private Vector2 backgroundSize;

    void Start()
    {
        backgroundSize = background.sizeDelta;
        ResetHandle();
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float x = localPoint.x / (backgroundSize.x * 0.5f);
            float y = localPoint.y / (backgroundSize.y * 0.5f);
            Vector2 raw = new Vector2(x, y);

            // clamp dentro do c�rculo
            input = raw.magnitude > 1 ? raw.normalized : raw;

            // move a bolinha
            handle.anchoredPosition = input * (backgroundSize * 0.5f * handleLimit);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        ResetHandle();
    }

    void ResetHandle()
    {
        handle.anchoredPosition = Vector2.zero;
    }

    // acesso ao input
    public Vector2 InputDirection => input;
    public float Horizontal => input.x;
    public float Vertical => input.y;
}
