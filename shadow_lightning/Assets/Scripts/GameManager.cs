using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string transitionedFromScene;

    public HeartSystem heartSystem;
    public int alive_enemy_count;

    public float unpossessTimer;
    public GameObject currentPlayer;
    public bool PlayerHasWallJump;
    PlayerMovement pm;
    
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
        if (currentPlayer){
            if (PlayerHasWallJump){
                pm = currentPlayer.GetComponent<PlayerMovement>();
                pm.wallJumpClimb = new Vector2(7.5f, 16);
                pm.wallJumpOff = new Vector2(8.5f, 7);
                pm.wallLeap = new Vector2(18, 17);
            }
        }else{
            currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
