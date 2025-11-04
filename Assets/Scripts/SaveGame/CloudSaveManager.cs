using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await InitializeServices();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async Task InitializeServices()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log("✅ Unity Cloud Save conectado e autenticado.");
    }

    public async Task SaveAsync(PlayerSaveData data)
    {
        var dict = new Dictionary<string, object> { { "PlayerSave", data } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(dict);
        Debug.Log("💾 Jogo salvo no Unity Cloud!");
    }

    public async Task<PlayerSaveData> LoadAsync()
    {
        try
        {
            var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "PlayerSave" });
            if (result.TryGetValue("PlayerSave", out var json))
            {
                PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json.ToString());
                Debug.Log("☁️ Save carregado do Cloud!");
                return data;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("⚠️ Nenhum save encontrado: " + ex.Message);
        }
        return null;
    }

    public async Task DeleteAsync()
    {
        await CloudSaveService.Instance.Data.ForceDeleteAsync("PlayerSave");
        Debug.Log("🗑️ Save deletado do Cloud.");
    }
}
