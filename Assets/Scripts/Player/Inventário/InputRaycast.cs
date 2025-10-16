using UnityEngine;

public class InputRaycast : MonoBehaviour
{
    //Camera cam;

    //void Start()
    //{
    //    cam = Camera.main; // pega a câmera principal
    //}

    //void Update()
    //{
    //    // ??? Clique com o mouse (PC)
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        DetectarCliqueOuToque(Input.mousePosition);
    //    }

    //    // ?? Toque na tela (Mobile)
    //    if (Input.touchCount > 0)
    //    {
    //        Touch toque = Input.GetTouch(0);
    //        if (toque.phase == TouchPhase.Began)
    //        {
    //            DetectarCliqueOuToque(toque.position);
    //        }
    //    }
    //}

    //void DetectarCliqueOuToque(Vector3 posicaoTela)
    //{
    //    Ray raio = cam.ScreenPointToRay(posicaoTela);
    //    RaycastHit hit;

    //    if (Physics.Raycast(raio, out hit))
    //    {
    //        Debug.Log("Tocou/Click em: " + hit.collider.name);

    //        // Exemplo: se o objeto tiver um script específico
    //        SlotUI interagivel = hit.collider.GetComponent<SlotUI>();
    //        if (interagivel != null)
    //        {
    //            interagivel.ClicarNoSlot();
    //        }
    //    }
    //}
}

