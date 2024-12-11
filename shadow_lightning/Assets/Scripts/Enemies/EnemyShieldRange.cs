using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShieldRange : MonoBehaviour
{
    public ShieldBash ShieldBash;
    public EnemyMovement EnemyMovement;
    public Animator Animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            ShieldBash.inRange = true;
            Animator.SetBool("Bashing", false);
            EnemyMovement.moveTowardsPlayer = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            ShieldBash.inRange = false;
            EnemyMovement.moveTowardsPlayer = true;
        }
    }
}