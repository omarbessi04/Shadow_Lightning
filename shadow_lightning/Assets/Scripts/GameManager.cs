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
    
    void Awake()
    {
        instance = this;
    }
}
