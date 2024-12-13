using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour
{
    public string typeEnemy;
    public bool boss = false;

    private void Start(){
        GameManager.instance.alive_enemy_count += 1;
    }
}
