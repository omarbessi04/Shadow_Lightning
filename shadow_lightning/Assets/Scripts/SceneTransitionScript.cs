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

    public void ResetGame(){
        GameObject shadow = GameObject.FindGameObjectWithTag("Player");
        shadow.transform.position = new Vector3(-27.49f, 1.5f, 1f);
        TeleportTo("Screen1");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerEnemy")) return;
        GameManager.instance.transitionedFromScene = SceneManager.GetActiveScene().name;
        TeleportTo(trantitionTo);

    }

    public void TeleportTo(string goTo){
        SceneManager.LoadScene(goTo);
        GameManager.instance.alive_enemy_count = 0;
    }
}
