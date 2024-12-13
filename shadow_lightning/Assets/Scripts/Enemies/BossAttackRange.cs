using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackRange : MonoBehaviour
{
    public BossAttack BossAttack;
    public BossMovement EnemyMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            BossAttack.inRange = true;
            EnemyMovement.moveTowardsPlayer = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            BossAttack.inRange = false;
            EnemyMovement.moveTowardsPlayer = true;
        }
    }
}