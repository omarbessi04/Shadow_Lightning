using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBashPlayer : MonoBehaviour
{
    public float damage;
    public float stunDuration = 3f;
    public float knockBack = 5f;
    public PlayerMovement Movement;
    public PlayerAnimator PlayerAnimator;
    public float bashSpeed = 40f;
    public float Timer;
    public float BashCooldown = 3f;
    public bool bashingRight;
    private Animator Animator;
    public float airResistince;
    private Coroutine stunCoroutine;
    public bool stunned = false;
    public List<GameObject> enemiesHit = new List<GameObject>();
    public bool bashing = false;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Animator.SetFloat("Xvelocity", Mathf.Abs(Movement.velocity.x));
        if (stunned)
        {
            Movement.velocity.x = 0;
        }
        Timer -= Time.deltaTime;
        if (!bashing && Timer < 0 && Input.GetAxisRaw("Ability") == 1)
        {
            Animator.SetBool("Bashing", true);
        }

        if (bashing)
        {
            if (bashingRight)
            {
                Movement.velocity.x -= airResistince;
                if (Movement.velocity.x < 0)
                {
                    animationDone();
                }
            }
            else
            {
                Movement.velocity.x += airResistince;
                if (Movement.velocity.x > 0)
                {
                    animationDone();
                }
                
            }
        }


    }

    public void enemyHit(GameObject enemy)
    {
        if (!enemiesHit.Contains(enemy) && enemy.tag == "Enemy")
        {
            enemy.GetComponent<EnemyHealth>().takeDamage(damage);
            enemy.GetComponent<EnemyMovement>().knockBack(knockBack);
            enemiesHit.Add(enemy);
        }
    }

    public void stunCheck()
    {
        if (!stunned)
        {
            Animator.SetBool("Stunned", true);
            Animator.SetBool("Bashing", false);
            bashing = false;
            Movement.velocity.x = 0;
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(Stun());
        }
    }

    IEnumerator Stun()
    {
        stunned = true;
        yield return new WaitForSeconds(stunDuration);
        Timer = BashCooldown;
        stunned = false;
        Animator.SetBool("Stunned", false);


    }
    
    
    public void animationDone()
    {
        //print("done");
        enemiesHit.Clear();
        Movement.velocity.x = 0;
        Movement.canMove = true;
        bashing = false;
        Animator.SetBool("Bashing", false);
    }
    public void Bash()
    {
        if (bashing == false)
        {
            Movement.canMove = false;
            Timer = BashCooldown;
            bashing = true;
            //print("BASHING");
            print(PlayerAnimator.lookingRight);
            if (PlayerAnimator.lookingRight)
            {
                Movement.velocity.x = +bashSpeed;
                bashingRight = true;
            }
            else
            {
                Movement.velocity.x = -bashSpeed;
                bashingRight = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boulder" && bashing)
        {
            other.GameObject().GetComponent<BoulderShake>().Begin();
        }
    }
}
