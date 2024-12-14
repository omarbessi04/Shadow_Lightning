using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounterTextScript : MonoBehaviour
{
    [SerializeField] TextMeshPro enemyCounter;
    void Update()
    {
        if (GameManager.instance.alive_enemy_count != 0){
        enemyCounter.text = GameManager.instance.alive_enemy_count.ToString();
        }else{
            enemyCounter.text = "";
        }
    }
}
