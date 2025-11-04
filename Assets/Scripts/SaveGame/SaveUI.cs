using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button deleteButton;

    [Header("Referências")]
    [SerializeField] private Transform player;
    [SerializeField] private Status status;
    [SerializeField] private GanhodeXp xp;

    private void Start()
    {
        saveButton.onClick.AddListener(async () => await SaveGame());
        loadButton.onClick.AddListener(async () => await LoadGame());
        deleteButton.onClick.AddListener(async () => await DeleteGame());
    }

    private async Task SaveGame()
    {
        PlayerSaveData data = new PlayerSaveData(player.position, status, xp);
        await CloudSaveManager.Instance.SaveAsync(data);
    }

    private async Task LoadGame()
    {
        PlayerSaveData data = await CloudSaveManager.Instance.LoadAsync();

        if (data != null)
        {
            // Reposiciona o jogador
            player.position = data.GetPosition();

            // Atualiza o Status
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

            // Atualiza o XP e nível
            typeof(GanhodeXp).GetField("xpAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.xpAtual);
            typeof(GanhodeXp).GetField("xpNecessarioParaNivelUp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.xpNecessario);
            typeof(GanhodeXp).GetField("levelAtual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(xp, data.levelAtual);

            Debug.Log("✅ Jogo carregado e aplicado com sucesso!");
        }
        else
        {
            Debug.Log("Nenhum save encontrado no Cloud.");
        }
    }

    private async Task DeleteGame()
    {
        await CloudSaveManager.Instance.DeleteAsync();
    }
}
