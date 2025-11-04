using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject savePanel;

    private void Start()
    {
        savePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            savePanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            savePanel.SetActive(false);
    }
}
