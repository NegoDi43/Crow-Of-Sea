using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SaveUI : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public Button deleteButton;

    [Header("Referências")]
    public Transform player;
    public Status status;
    public GanhodeXp xp;

    private void Start()
    {
        saveButton.onClick.AddListener(async () => await SaveGame());
        loadButton.onClick.AddListener(async () => await LoadGame());
        deleteButton.onClick.AddListener(async () => await DeleteGame());
    }

    private async Task SaveGame()
    {
        PlayerSaveData data = new PlayerSaveData(
            player.position,
            SceneManager.GetActiveScene().name,
            status,
            xp
        );
        await CloudSaveManager.Instance.SaveAsync(data);
    }

    private async Task LoadGame()
    {
        PlayerSaveData data = await CloudSaveManager.Instance.LoadAsync();
        if (data == null) return;

        // Se o save for de outra cena, carrega a cena antes
        if (data.sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(data.sceneName);
            await Task.Delay(500);
        }

        // Atualiza dados
        player.position = data.GetPosition();
        typeof(Status).GetField("vidaMaxima", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.vidaMaxima);
        typeof(Status).GetField("vidaAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.vidaAtual);
        typeof(Status).GetField("danoMaximo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.danoMaximo);
        typeof(Status).GetField("velocidade", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.velocidade);
        typeof(Status).GetField("staminaMax", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.staminaMax);
        typeof(Status).GetField("staminaAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.staminaAtual);
        typeof(Status).GetField("pontos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.pontos);
        typeof(Status).GetField("pontosVida", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.pontosVida);
        typeof(Status).GetField("pontosDano", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.pontosDano);
        typeof(Status).GetField("pontosVelocidade", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.pontosVelocidade);
        typeof(Status).GetField("pontosStamina", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(status, data.pontosStamina);

        typeof(GanhodeXp).GetField("xpAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.xpAtual);
        typeof(GanhodeXp).GetField("xpNecessarioParaNivelUp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.xpNecessario);
        typeof(GanhodeXp).GetField("levelAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.levelAtual);

        Debug.Log("✅ Jogo carregado com sucesso!");
    }

    private async Task DeleteGame()
    {
        await CloudSaveManager.Instance.DeleteAsync();
    }
}
