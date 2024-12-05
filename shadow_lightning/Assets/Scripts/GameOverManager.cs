using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour{

    [SerializeField] private string startScreen;

    public void RetryButtonPressed(){
        SceneTransitionScript.instance.TeleportTo(startScreen);
    }

    public void QuitButtonPressed(){
        Application.Quit();
    }
}