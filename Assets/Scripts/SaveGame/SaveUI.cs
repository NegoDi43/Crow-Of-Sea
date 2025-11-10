using UnityEngine;
using System.Threading.Tasks;

public class SaveUI : MonoBehaviour
{
    public PlayerStatus player;
    private const string SAVE_SLOT = "player_save";

    public async void SaveGame()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        }

        string json = JsonUtility.ToJson(player.GetSaveData());
        await CloudSaveManager.Instance.SaveAsync(SAVE_SLOT, json);
    }

    public async void LoadGame()
    {
        string json = await CloudSaveManager.Instance.LoadAsync(SAVE_SLOT);
        if (!string.IsNullOrEmpty(json))
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
            player.LoadFromData(data);
        }
    }

    public async void DeleteSave()
    {
        await CloudSaveManager.Instance.DeleteAsync(SAVE_SLOT);
    }
}