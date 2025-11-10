using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MainMenuUI : MonoBehaviour
{
    private const string SAVE_SLOT = "player_save";
    [SerializeField] private AnimaçãoButton animaçãoButtonNewGame;
    [SerializeField] private AnimaçãoButton animaçãoButtonLoadGame;
    [SerializeField] private int counter = 0;
    void Update()
    {
        if (counter >= 1)
        {
            animaçãoButtonNewGame.Fechar();
            animaçãoButtonLoadGame.Fechar();
        }
    }
    public void NewGameButton()
    {
        StartCoroutine(TempoNewGame());
    }

    IEnumerator TempoNewGame()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        NewGame();
    }

    public async void NewGame()
    {
        await CloudSaveManager.Instance.DeleteAsync(SAVE_SLOT);
        SceneManager.LoadScene("Fase1"); // 🔁 troque pelo nome da primeira fase
    }


    public void LoadGameButton()
    {
        StartCoroutine(TempoLoadGame());
    }
    IEnumerator TempoLoadGame()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        LoadGame();
    }
    public async void LoadGame()
    {
        string json = await CloudSaveManager.Instance.LoadAsync(SAVE_SLOT);
        if (!string.IsNullOrEmpty(json))
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.Log("Nenhum save encontrado!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}