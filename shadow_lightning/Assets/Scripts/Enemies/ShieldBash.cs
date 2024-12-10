using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public float knockBack = 5f;
    public EnemyMovement EnemyMovement;
    public float bashRange = 5f;
    public float minBashRange = 3f;
    public EnemyDetection Detection;
    public LayerMask mask;
    public float bashSpeed = 40f;
    public float Timer;
    public float BashCooldown = 3f;
    public bool bashingRight;
    
    public bool bashing = false;
    private void FixedUpdate()
    {
        Timer -= Time.deltaTime;
        RaycastHit2D hit;
        if (Detection.Detected)
        {
            if (EnemyMovement.flippedRight)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right, bashRange, mask);
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, Vector2.left, bashRange, mask);
            }

            if (hit && Mathf.Abs(hit.distance) > minBashRange)
            {
                if (!bashing && Timer < 0)
                {
                    Bash();
                }
            }
        }

        if (bashing)
        {
            if (bashingRight)
            {
                EnemyMovement.velocity.x -= 1;
                if (EnemyMovement.velocity.x < 0)
                {
                    bashing = false;
                    EnemyMovement.noVelocityReset = false;
                }
            }
            else
            {
                EnemyMovement.velocity.x += 1;
                if (EnemyMovement.velocity.x > 0)
                {
                    bashing = false;
                    EnemyMovement.noVelocityReset = false;
                }
                
            }
        }


    }

    public void Bash()
    {
        if (bashing == false)
        {
            Timer = BashCooldown;
            bashing = true;
            EnemyMovement.noVelocityReset = true;
            print("BASHING");
            if (EnemyMovement.flippedRight)
            {
                EnemyMovement.velocity.x = +bashSpeed;
                bashingRight = true;
            }
            else
            {
                EnemyMovement.velocity.x = -bashSpeed;
                bashingRight = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerEnemy")
        {
            if (bashing)
            {
                EnemyMovement.velocity.x = 0;
                EnemyMovement.noVelocityReset = false;
                bashing = false;
                if (bashingRight)
                {
                    other.GameObject().GetComponent<PlayerMovement>().knockBack(knockBack);
                }
                else
                {
                    other.GameObject().GetComponent<PlayerMovement>().knockBack(-knockBack);
                }
            }
        }
    }
}
