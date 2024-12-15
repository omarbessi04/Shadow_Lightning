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
    private bool playerInLeft;
    private bool playerInRight;

    private void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left)
            {
                BossAttack.playerInLeftAttack = true;
            }
            else if (!Left)
            {
                BossAttack.playerInRightAttack = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left)
            {
                BossAttack.playerInLeftAttack = true;
            }
            else if (!Left)
            {
                BossAttack.playerInRightAttack = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (Left)
            {
                BossAttack.playerInLeftAttack = false;
            }
            else if (!Left)
            {
                BossAttack.playerInRightAttack = false;
            }
        }
    }

    // Update the playerInAttack flag based on the left and right states
}
