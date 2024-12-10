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
    private GameObject Player;
    public bool inRange = false;
    private Animator Animator;
    public float airResistince;
    
    public bool bashing = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!EnemyMovement.idle)
        {
            if (!bashing)
            {
                EnemyMovement.velocity.x = 0;
            }
            EnemyMovement.noVelocityReset = true;
        }
        else
        {
            EnemyMovement.noVelocityReset = false;
        }
        if (Detection.Detected && Player == null)
        {
            Player = GameObject.FindWithTag("PlayerEnemy");
        }

        if (Detection.Detected)
        {
            float distanceToPlayer = (transform.position.x - Player.transform.position.x);
            if (Animator.GetBool("Attacking") == false)
            {
                if (distanceToPlayer > 0)
                {
                    EnemyMovement.flipX(true);
                }
                else
                {
                    EnemyMovement.flipX(false);
                }
            }
            
        }
        Timer -= Time.deltaTime;
        RaycastHit2D hit;
        if (Detection.Detected && inRange)
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
            print(EnemyMovement.velocity.x);
            RaycastHit2D hit2;
            if (EnemyMovement.flippedRight)
            {
                hit2 = Physics2D.Raycast(transform.position, Vector2.right, 0, mask);
            }
            else
            {
                hit2 = Physics2D.Raycast(transform.position, Vector2.left, 0, mask);
            }

            if (hit2)
            {
                print("playerhit");
                EnemyMovement.velocity.x = 0;
                bashing = false;
                if (bashingRight)
                {
                    hit2.collider.GameObject().GetComponent<PlayerMovement>().knockBack(knockBack);
                }
                else
                {
                    hit2.collider.GameObject().GetComponent<PlayerMovement>().knockBack(-knockBack);
                }
                GameManager.instance.heartSystem.TakeDamage(0.5f);
            }
            if (bashingRight)
            {
                EnemyMovement.velocity.x -= airResistince;
                if (EnemyMovement.velocity.x < 0)
                {
                    print("CURRENT VEL: " + EnemyMovement.velocity.x);
                    bashing = false;
                    EnemyMovement.velocity.x = 0;
                    print("RESET1");
                }
            }
            else
            {
                EnemyMovement.velocity.x += airResistince;
                if (EnemyMovement.velocity.x > 0)
                {
                    print("CURRENT VEL: " + EnemyMovement.velocity.x);
                    bashing = false;
                    EnemyMovement.velocity.x = 0;
                    print("RESET2");
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
    
}
