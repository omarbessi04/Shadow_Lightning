using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameScreen;
    [SerializeField] private string creditsScreen;
    [SerializeField] private string menuScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScreen);
        GameManager.instance.currentScene = gameScreen;
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(creditsScreen);
        GameManager.instance.currentScene = creditsScreen;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuScreen);
        GameManager.instance.currentScene = menuScreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
