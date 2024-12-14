using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScreen);
        GameManager.instance.currentScene = gameScreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
