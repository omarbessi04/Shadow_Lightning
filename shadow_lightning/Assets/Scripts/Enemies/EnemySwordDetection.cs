using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySwordDetection : MonoBehaviour
{
    public EnemySwordAttack EnemySwordAttack;
    public bool Left;
    public EnemyMovement EnemyMovement;

    private EdgeCollider2D collider;

    private void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (EnemySwordAttack.Animator.GetBool("Attacking") == false)
        {
            collider.enabled = false;
        }
        else if (EnemySwordAttack.Animator.GetBool("Attacking") == true)
        {
            collider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left && !EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = true;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left && !EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = true;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left && !EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = false;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                EnemySwordAttack.playerInAttack = false;
            }
        }
    }
}
