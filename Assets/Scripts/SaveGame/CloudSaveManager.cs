using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance;

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await InitializeUnityServices();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async Task InitializeUnityServices()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"[CloudSaveManager] Logado anonimamente: {AuthenticationService.Instance.PlayerId}");
        }
    }

    public async Task SaveAsync(string slot, string json)
    {
        try
        {
            var data = new Dictionary<string, object> { { slot, json } };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log("[CloudSaveManager] Dados salvos com sucesso!");
        }
        catch (CloudSaveException e)
        {
            Debug.LogError($"[CloudSaveManager] Erro ao salvar: {e.Message}");
        }
    }

    public async Task<string> LoadAsync(string slot)
    {
        try
        {
            var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { slot });
            if (result.TryGetValue(slot, out string json))
                return json;
            else
                return null;
        }
        catch (CloudSaveException e)
        {
            Debug.LogWarning($"[CloudSaveManager] Nenhum save encontrado: {e.Message}");
            return null;
        }
    }

    public async Task DeleteAsync(string slot)
    {
        try
        {
            await CloudSaveService.Instance.Data.DeleteAsync(slot);
            Debug.Log("[CloudSaveManager] Save deletado com sucesso!");
        }
        catch (CloudSaveException e)
        {
            Debug.LogError($"[CloudSaveManager] Erro ao deletar: {e.Message}");
        }
    }
}