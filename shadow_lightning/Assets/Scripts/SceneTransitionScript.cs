using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitionScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string trantitionTo;
    [SerializeField] private float exitTime;
    [SerializeField] Transform startPoint;
    [SerializeField] Vector2 exitDirection;

    private void Start(){
        if (trantitionTo == GameManager.instance.transitionedFromScene){
            GameObject shadow = GameObject.FindGameObjectWithTag("Player");
            shadow.transform.position = startPoint.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("PlayerEnemy")) return;

        if (GameManager.instance.alive_enemy_count != 0) return;
        GameManager.instance.transitionedFromScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(trantitionTo);

    }
}
