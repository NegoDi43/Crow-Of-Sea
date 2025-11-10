using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public string firstSceneName = "Fase1"; // nome da cena inicial do jogo
    [SerializeField] private AnimaçãoButton animaçãoButtonLeaderBoard;
    [SerializeField]private AnimaçãoButton animaçãoButtonQuitGame;
    [SerializeField]private AnimaçãoButton animaçãoButtonCredits;
    [SerializeField] private int counter = 0;

    void Start()
    { 
    }

    void Update()
    {
        if (counter >= 1)
        {
            animaçãoButtonQuitGame.Fechar();
            animaçãoButtonCredits.Fechar();
            animaçãoButtonLeaderBoard.Fechar();
        }
    }


    public void QuitGameButton()
    {
        StartCoroutine(TempoQuitGame());
    }

    IEnumerator TempoQuitGame()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        QuitGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsGameButton()
    {
        StartCoroutine(TempoCredits());
    }

    IEnumerator TempoCredits()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        OpenCredits();
    }
    private void OpenCredits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void MenuGameButton()
    {
        StartCoroutine(TempoMenu());
    }

    IEnumerator TempoMenu()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        OpenMenu();
    }
    private void OpenMenu()
    {
        SceneManager.LoadScene("TelaInicial");
    }

    public void LeaderBoardButton()
    {
        StartCoroutine(TempoLeaderBoard());
    }

    IEnumerator TempoLeaderBoard()
    {
        counter++;
        yield return new WaitForSeconds(0.5f);
        LeaderBoard();
    }
    private void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
}
