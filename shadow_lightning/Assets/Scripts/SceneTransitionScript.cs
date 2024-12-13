using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitionScript : MonoBehaviour
{
    // Start is called before the first frame update

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

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerEnemy")) return;
        TeleportTo(trantitionTo);

    }

    public void TeleportTo(string goTo){
        GameManager.instance.transitionedFromScene = SceneManager.GetActiveScene().name;
        GameManager.instance.currentScene = trantitionTo;
        
        GameManager.instance.alive_enemy_count = 0;

        SceneManager.LoadScene(goTo);
    }
}
