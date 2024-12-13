using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour{

    [SerializeField] private string startScreen;

    public void RetryButtonPressed(){
        SceneTransitionScript.instance.ResetGame();
    }

    public void QuitButtonPressed(){
        Application.Quit();
    }
}