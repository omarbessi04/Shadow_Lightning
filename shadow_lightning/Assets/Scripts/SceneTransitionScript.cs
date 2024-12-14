using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneTransitionScript : MonoBehaviour
{
    [SerializeField] private string trantitionTo;
    [SerializeField] Transform startPoint;
    public static SceneTransitionScript instance;

    void Awake()
    {
        instance = this;
    }

    private void Start(){
        if (trantitionTo == GameManager.instance.transitionedFromScene){
            GameObject shadow = GameObject.FindGameObjectWithTag("Player");
            
            shadow.transform.position = startPoint.transform.position;
        }
    }

    private void Update() {
        if (GameManager.instance.currentScene == ""){
            GameManager.instance.currentScene = SceneManager.GetActiveScene().name;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerEnemy")) return;
        TeleportTo(trantitionTo);

    }

    public void Restart(){
        SceneManager.LoadScene("GameOver");
    }

    public void TeleportTo(string goTo){
        GameManager.instance.transitionedFromScene = SceneManager.GetActiveScene().name;
        GameManager.instance.currentScene = trantitionTo;
        
        GameManager.instance.alive_enemy_count = 0;

        SceneManager.LoadScene(goTo);
    }
}
