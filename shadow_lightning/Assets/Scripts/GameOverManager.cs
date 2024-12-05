using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour{

    [SerializeField] private string startScreen;

    public void ButtonPressed(){
        SceneTransitionScript.instance.TeleportTo(startScreen);
    }
}