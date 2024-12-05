using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameScreen;
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScreen);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
