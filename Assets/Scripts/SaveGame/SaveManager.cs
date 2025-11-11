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

    // --- FUNÇÃO DE SALVAR ---
    public async void SaveGame()
    {
        SaveData data = new SaveData();

        // 1. Coletar dados do Status
        data.vidaMaxima = statusPlayer.GetVidaMaxima();
        data.vidaAtual = statusPlayer.GetVidaAtual();
        data.danoMaximo = statusPlayer.GetDanoMaximo();
        data.velocidade = statusPlayer.GetVelocidade();
        data.staminaAtual = statusPlayer.GetStaminaAtual();
        data.staminaMax = statusPlayer.GetStaminaMax();
        data.pontos = statusPlayer.GetPontos();
        data.pontosVida = statusPlayer.GetPontosVida();
        data.pontosDano = statusPlayer.GetPontosDano();
        data.pontosVelocidade = statusPlayer.GetPontosVelocidade();
        data.pontosStamina = statusPlayer.GetPontosStamina();

        // 2. Coletar dados do GanhodeXp
        data.xpAtual = xpPlayer.GetXpAtual();
        data.xpNecessarioParaNivelUp = xpPlayer.GetXpNecessarioParaNivelUp();
        data.levelAtual = xpPlayer.GetLevelAtual(); // <-- CORRIGIDO para usar o Getter

        // 3. Coletar Posição
        data.playerPosX = playerTransform.position.x;
        data.playerPosY = playerTransform.position.y;

        // 4. Coletar Inventário
        data.inventorySlots = new List<SerializableSlot>();
        foreach (SlotInventario slot in inventarioPlayer.slots)
        {
            // Salva o ID (nome) e a quantidade
            data.inventorySlots.Add(new SerializableSlot(slot.item.nomeItem, slot.quantidade));
        }

        // 5. Enviar para a Nuvem
        try
        {
            string json = JsonUtility.ToJson(data);
            var dataToSave = new Dictionary<string, object> { { SAVE_KEY, json } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);
            Debug.Log("Jogo Salvo na Nuvem!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar: " + e.Message);
        }
    }

    // --- FUNÇÃO DE CARREGAR ---
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
                inventarioPlayer.slots.Clear();
                foreach (SerializableSlot sSlot in data.inventorySlots)
                {
                    // Encontra o PrefabsItens real usando o ID salvo
                    PrefabsItens itemReal = itemDatabase.GetItemPorID(sSlot.itemID);
                    if (itemReal != null)
                    {
                        inventarioPlayer.slots.Add(new SlotInventario(itemReal, sSlot.quantidade));
                    }
                }
                // Atualiza a UI do inventário
                inventarioPlayer.inventarioUI.AtualizarUI();

                Debug.Log("Jogo Carregado da Nuvem!");
            }
            else
            {
                Debug.Log("Nenhum save encontrado. Carregando dados padrão.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao carregar: " + e.Message);
        }
    }
}