using UnityEngine;
using UnityEngine.SceneManagement; // Para mudar de cena
using System.Threading.Tasks;      // Para usar o "Task" (aguardar)
using Unity.Services.CloudSave;    // Para apagar o save

/// <summary>
/// Controla os botões do menu principal, como Novo Jogo e Carregar Jogo.
/// </summary>
public class MenuPrincipalController : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("O nome da cena principal do seu jogo (ex: 'Level1', 'GameScene')")]
    [SerializeField] private string nomeDaCenaDoJogo = "Level1";

    // A chave do save. DEVE ser a mesma que está no seu SaveManager.cs!
    private const string SAVE_KEY = "PlayerData";

    /// <summary>
    /// Esta função deve ser chamada pelo seu botão "Novo Jogo".
    /// </summary>
    public async void BotaoNovoJogo()
    {
        Debug.Log("Iniciando Novo Jogo... Apagando save anterior, se existir.");

        try
        {
            // 1. Tenta apagar os dados da nuvem
            await CloudSaveService.Instance.Data.Player.DeleteAsync(SAVE_KEY);
            Debug.Log("Save anterior apagado com sucesso.");
        }
        catch (System.Exception e)
        {
            // Um erro aqui pode significar que não havia save, o que é normal.
            Debug.LogWarning($"Não foi possível apagar o save (pode não existir): {e.Message}");
        }

        // --- 2. RESETA O CONTADOR DE TEMPO ---
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.ResetPlaytime();
        }
        else
        {
            Debug.LogError("Não foi possível encontrar o SaveManager para resetar o tempo!");
        }
        // -------------------------------------

        // 3. Carrega a cena principal do jogo
        SceneManager.LoadScene(nomeDaCenaDoJogo);
    }

    /// <summary>
    /// Esta função deve ser chamada pelo seu botão "Carregar Jogo".
    /// </summary>
    public async void BotaoCarregarJogo()
    {
        Debug.Log("Carregando Jogo Salvo...");

        if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager.Instance não foi encontrado! O SaveManager está na cena?");
            return;
        }

        try
        {
            // 1. Espera o SaveManager carregar os dados
            await SaveManager.Instance.LoadGame();

            Debug.Log("Dados carregados com sucesso. Iniciando a cena do jogo.");

            // 2. Só depois que os dados foram carregados, muda para a cena do jogo
            SceneManager.LoadScene(nomeDaCenaDoJogo);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao carregar o jogo: {e.Message}");
            // Aqui você poderia mostrar uma mensagem de erro para o jogador na UI
        }
    }
}