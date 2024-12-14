using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCounterScript : MonoBehaviour
{
    public TextMeshProUGUI countdown; 
    public int myTime = 3;

    private void Awake() {
        StartCoroutine(myTimer());
    }

    IEnumerator myTimer() {
        while(myTime > -1) {
            countdown.text = $"Respawning in {myTime}";
            myTime -= 1;
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(GameManager.instance.currentScene);
    }

}
