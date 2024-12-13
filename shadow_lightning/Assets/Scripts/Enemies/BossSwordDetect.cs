using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSwordDetect : MonoBehaviour
{
    public BossAttack BossAttack;
    public bool Left;
    public BossMovement EnemyMovement;

    private EdgeCollider2D collider;

    private void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (BossAttack.Animator.GetBool("Attacking") == false)
        {
            collider.enabled = false;
        }
        else if (BossAttack.Animator.GetBool("Attacking") == true)
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
                BossAttack.playerInAttack = true;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                BossAttack.playerInAttack = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left && !EnemyMovement.flippedRight)
            {
                BossAttack.playerInAttack = true;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                BossAttack.playerInAttack = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left && !EnemyMovement.flippedRight)
            {
                BossAttack.playerInAttack = false;
            }
            else if (!Left && EnemyMovement.flippedRight)
            {
                BossAttack.playerInAttack = false;
            }
        }
    }
}