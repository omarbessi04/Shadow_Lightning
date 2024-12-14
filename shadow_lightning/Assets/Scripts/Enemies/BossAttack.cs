using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class BossAttack : MonoBehaviour
{
    public GameObject waveAttack;
    public float damage = 1f;
    public bool inRange = false;
    public float attackCooldown;
    public BossDetection BossDetection;
    private GameObject Player = null;
    private BossMovement Movement;
    public Animator Animator;

    public Transform marker2D;
    private bool isAttacking = false;

    public bool playerHit = false;
    public bool playerInAttack;
    AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}


    void Start()
    {
        Movement = GetComponent<BossMovement>();
    }
    
    void FixedUpdate()
    {
        if (BossDetection.Detected && Player == null)
        {
            Player = GameObject.FindWithTag("PlayerEnemy");
        }

        if (BossDetection.Detected)
        {
            if (Animator.GetBool("Attacking") == false)
            {
                if (Player)
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
        }
        
        if (inRange)
        {
            Movement.moveTowardsPlayer = false;
        }
        else
        {
            Movement.moveTowardsPlayer = true;
        }

        if (BossDetection.Detected && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
            
        }
    }
    

    IEnumerator AttackRoutine()
    {
        isAttacking = true; 
        while (BossDetection.Detected)
        {
            Animator.SetBool("Attacking", true);
            if (inRange)
            {
                yield return new WaitForSeconds(attackCooldown/2);
            }
            else
            {
                yield return new WaitForSeconds(attackCooldown);    
            }
            
            
        }
        isAttacking = false; 
    }
    

    public void AnimationReset()
    {
        Animator.SetBool("Attacking", false);
        playerHit = false;
    }

    public void audioSFX()
    {
        audioManager.PlaySFX(audioManager.SwordHit);
    }

    public void Attack()
    {
        if (!playerHit && playerInAttack)
        {
            GameManager.instance.heartSystem.TakeDamage(damage);
            playerHit = true;
        }
        if (Movement.flippedRight)
        {
            waveAttack.GetComponent<ElectricAttack>().direction = Vector2.right;
        }
        else
        {
            waveAttack.GetComponent<ElectricAttack>().direction = Vector2.left;
        }

        Instantiate(waveAttack, marker2D.transform.position, quaternion.identity);
    }
}