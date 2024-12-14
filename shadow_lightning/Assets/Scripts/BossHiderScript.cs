using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class BossHiderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(GameManager.instance.BossHasBeenKilled){

            GameObject boss = GameObject.Find("BossSword");
            if (!boss) return;
            
            boss.SetActive(false);
        }
    }
}
