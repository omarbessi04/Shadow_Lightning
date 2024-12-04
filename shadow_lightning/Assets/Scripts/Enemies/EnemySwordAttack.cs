using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemySwordAttack : MonoBehaviour
{
    public float damage = 1f;
    public bool inRange = false;
    public float attackCooldown;
    public EnemyDetection enemyDetect;
    private GameObject Player = null;
    private EnemyMovement Movement;
    public Animator Animator;

    public Transform marker2D;
    private bool isAttacking = false;

    public bool playerHit = false;
    public bool playerInAttack;

    void Start()
    {
        Movement = GetComponent<EnemyMovement>();
    }
    

    void FixedUpdate()
    {
        if (enemyDetect.Detected && Player == null)
        {
            Player = GameObject.FindWithTag("PlayerEnemy");
        }

        if (enemyDetect.Detected)
        {
            if (Animator.GetBool("Attacking") == false)
            {
                float distanceToPlayer = (transform.position.x - Player.transform.position.x);
                if (distanceToPlayer > 0)
                {
                    Movement.flipX(true);
                }
                else
                {
                    Movement.flipX(false);
                }
            }
        }
        
        if (inRange)
        {
            Movement.moveTowardsPlayer = false;
        }
        else
        {
            Movement.moveTowardsPlayer = true;
        }

        if (enemyDetect.Detected && !isAttacking && inRange)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    

    IEnumerator AttackRoutine()
    {
        isAttacking = true; 
        while (enemyDetect.Detected && inRange)
        {
            Animator.SetBool("Attacking", true);
            yield return new WaitForSeconds(attackCooldown);
            
        }
        isAttacking = false; 
    }
    

    public void AnimationReset()
    {
        Animator.SetBool("Attacking", false);
        playerHit = false;
    }

    public void Attack()
    {
        if (!playerHit && playerInAttack)
        {
            GameManager.instance.heartSystem.TakeDamage(damage);
            playerHit = true;
        }
    }
}