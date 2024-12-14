using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string transitionedFromScene;
    public string currentScene;

    public HeartSystem heartSystem;
    public int alive_enemy_count;

    public float unpossessTimer;
    public GameObject currentPlayer;
    public bool canZoom = true;

    [Header("Progress Trackers")]
    public bool PlayerHasWallJump = false;
    public bool BoulderHasBeenDestroyed = false;
    public bool BossHasBeenKilled = false;
    PlayerMovement pm;

    public string currentStateofPlayer;
    
    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (GameObject.FindGameObjectWithTag("Player") && GameObject.FindGameObjectWithTag("Player").GetComponent<PossessController>().possessed)
        {
            currentStateofPlayer = "Enemy";
        }
        else
        {
            currentStateofPlayer = "Shadow";
        }
        if (currentPlayer){
            if (PlayerHasWallJump){
                if (currentStateofPlayer == "Shadow")
                {
                    pm = currentPlayer.GetComponent<PlayerMovement>();
                    pm.wallJumpClimb = new Vector2(7.5f, 16);
                    pm.wallJumpOff = new Vector2(8.5f, 7);
                    pm.wallLeap = new Vector2(18, 17);
                }
            }
        }else{
            currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void ResetGame(){
        PlayerHasWallJump = false;
        BoulderHasBeenDestroyed = false;
        BossHasBeenKilled = false;
    }

}
