using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // Referências para os scripts que GERENCIAM os dados
    [Header("Referências de Dados")]
    [SerializeField] private Status statusPlayer;
    [SerializeField] private GanhodeXp xpPlayer;
    [SerializeField] private Inventario inventarioPlayer;
    [SerializeField] private Transform playerTransform;

    // Referência crucial para CARREGAR o inventário
    [Header("Database de Itens")]
    [SerializeField] private ItemDatabase itemDatabase;

    private const string SAVE_KEY = "PlayerData";

    // --- NOVAS VARIÁVEIS DE TEMPO ---
    private float totalPlaytimeLoaded = 0f; // Tempo total carregado do save
    private float sessionStartTime;       // Momento em que a sessão (ou load) começou

    private async void Awake()
    {
        // Padrão Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Define o início da sessão. Será resetado no Load ou NewGame.
        sessionStartTime = Time.time;

        // Inicializa os serviços da Unity
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();

            // Login anônimo (mais simples)
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Player logado anonimamente: " + AuthenticationService.Instance.PlayerId);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao inicializar serviços Unity: " + e.Message);
        }
    }

    // --- FUNÇÃO DE SALVAR (MODIFICADA) ---
    public async void SaveGame()
    {
        SaveData data = new SaveData();

        // 1. Coletar dados do Status
        data.vidaMaxima = statusPlayer.GetVidaMaxima();
        data.vidaAtual = statusPlayer.GetVidaAtual();
        // ... (resto dos dados do Status) ...
        data.pontosStamina = statusPlayer.GetPontosStamina();

        // 2. Coletar dados do GanhodeXp
        data.xpAtual = xpPlayer.GetXpAtual();
        data.xpNecessarioParaNivelUp = xpPlayer.GetXpNecessarioParaNivelUp();
        data.levelAtual = xpPlayer.GetLevelAtual();

        // 3. Coletar Posição
        data.playerPosX = playerTransform.position.x;
        data.playerPosY = playerTransform.position.y;

        // 4. Coletar Inventário
        data.inventorySlots = new List<SerializableSlot>();
        foreach (SlotInventario slot in inventarioPlayer.slots)
        {
            data.inventorySlots.Add(new SerializableSlot(slot.item.nomeItem, slot.quantidade));
        }

        // --- 5. CALCULAR E COLETAR TEMPO JOGADO ---
        float sessionTime = Time.time - sessionStartTime;
        data.totalPlaytimeInSeconds = this.totalPlaytimeLoaded + sessionTime;
        // ---------------------------------------------

        // 6. Enviar para a Nuvem
        try
        {
            string json = JsonUtility.ToJson(data);
            var dataToSave = new Dictionary<string, object> { { SAVE_KEY, json } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);

            // Atualiza o tempo carregado e reseta o timer da sessão
            // para que o próximo save continue a partir daqui
            this.totalPlaytimeLoaded = data.totalPlaytimeInSeconds;
            this.sessionStartTime = Time.time;

            Debug.Log($"Jogo Salvo! Tempo total: {data.totalPlaytimeInSeconds}s");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar: " + e.Message);
        }
    }

    // --- FUNÇÃO DE CARREGAR (MODIFICADA) ---
    // Lembre-se que esta função deve retornar 'Task' e não 'void'
    public async Task LoadGame()
    {
        try
        {
            // 1. Baixar dados da Nuvem
            var results = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { SAVE_KEY });

            if (results.TryGetValue(SAVE_KEY, out var item))
            {
                // 2. Converter JSON de volta para o objeto SaveData
                string json = item.Value.GetAsString();
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                // 3. Aplicar dados 
                statusPlayer.LoadData(data);
                xpPlayer.LoadData(data);

                // 4. Aplicar Posição
                playerTransform.position = new Vector3(data.playerPosX, data.playerPosY, playerTransform.position.z);

                // 5. Aplicar Inventário
                // ... (código do inventário) ...
                inventarioPlayer.inventarioUI.AtualizarUI();

                // --- 6. CARREGAR TEMPO JOGADO ---
                this.totalPlaytimeLoaded = data.totalPlaytimeInSeconds;
                this.sessionStartTime = Time.time; // Reseta o timer da sessão
                // ---------------------------------

                Debug.Log($"Jogo Carregado! Tempo salvo anterior: {this.totalPlaytimeLoaded}s");
            }
            else
            {
                Debug.Log("Nenhum save encontrado. Carregando dados padrão.");
                // Se não há save, reseta o tempo (redundante, mas seguro)
                ResetPlaytime();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao carregar: " + e.Message);
            // Mesmo em caso de erro, é bom resetar o timer
            ResetPlaytime();
            throw; // Re-lança a exceção para o MenuPrincipalController saber que falhou
        }
    }

    // --- NOVO MÉTODO PÚBLICO ---
    /// <summary>
    /// Reseta o contador de tempo de jogo.
    /// Chamado pelo MenuPrincipalController ao iniciar um novo jogo.
    /// </summary>
    public void ResetPlaytime()
    {
        totalPlaytimeLoaded = 0f;
        sessionStartTime = Time.time;
        Debug.Log("Contador de tempo de jogo resetado.");
    }
}