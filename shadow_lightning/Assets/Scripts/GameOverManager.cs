using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour{

    [SerializeField] string lastScene = GameManager.instance.transitionedFromScene;

    public void RetryButtonPressed(){
        SceneTransitionScript.instance.TeleportTo(lastScene);
    }

    public void QuitButtonPressed(){
        Application.Quit();
    }
}