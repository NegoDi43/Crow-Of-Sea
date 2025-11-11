using UnityEngine;

/// <summary>
/// Este script salva o jogo automaticamente quando o jogador entra no trigger.
/// Deve ser colocado em um GameObject com um Collider2D marcado como "Is Trigger".
/// </summary>
[RequireComponent(typeof(Collider2D))] // Garante que há um collider
public class AutoSaveZone : MonoBehaviour
{
    // Variável para evitar saves múltiplos se o jogador ficar na zona
    private bool hasBeenTriggered = false;

    private void Start()
    {
        // Garante que o collider está configurado como Trigger
        Collider2D col = GetComponent<Collider2D>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"O Collider2D em {gameObject.name} não estava marcado como 'Is Trigger'. Corrigindo automaticamente.");
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se quem entrou é o "Player" (usando a Tag)
        // 2. Verifica se esta zona já não foi acionada (para salvar apenas uma vez)
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            // Tenta encontrar a instância do SaveManager
            if (SaveManager.Instance != null)
            {
                Debug.Log("AutoSave: Checkpoint alcançado, salvando o jogo!");
                SaveManager.Instance.SaveGame();

                // Marca como acionado para não salvar de novo
                hasBeenTriggered = true;

                // Opcional: Se você quiser que o checkpoint seja desativado após o uso:
                // gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("AutoSaveZone não conseguiu encontrar o SaveManager.Instance!");
            }
        }
    }

    /// <summary>
    /// Opcional: Se você quiser que o checkpoint seja re-utilizável,
    /// basta re-ativar o trigger quando o jogador sair.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reseta o trigger para que ele possa ser usado novamente
            hasBeenTriggered = false;
        }
    }
}