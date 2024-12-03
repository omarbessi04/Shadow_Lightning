using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyLightningAttack : MonoBehaviour
{
    public float attackCooldown;
    public EnemyDetection enemyDetect;
    public GameObject lightningAttack; 
    private GameObject Player = null;
    private EnemyMovement Movement;
    public Animator Animator;

    public Transform marker2D;
    private bool isAttacking = false; 

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

        if (enemyDetect.Detected && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true; 
        while (enemyDetect.Detected)
        {
            Animator.SetBool("Attacking", true);
            yield return new WaitForSeconds(attackCooldown);
            
        }
        isAttacking = false; 
    }

    public void AnimationReset()
    {
        Animator.SetBool("Attacking", false);
    }

    public void Attack()
    {
        Vector2 playerDirection = ((Player.transform.position - transform.position).normalized);
        if (playerDirection.x > 0)
        {
            playerDirection = new Vector2(1, 0);
        }
        else
        {
            playerDirection = new Vector2(-1, 0);
        }
        lightningAttack.GetComponent<ElectricAttack>().direction = new Vector2(playerDirection.x, 0);
        Instantiate(lightningAttack, marker2D.position, quaternion.identity);
    }
}