using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    public float stunDuration = 3f;
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
    public bool canFlip = true;
    private Coroutine stunCoroutine;
    public bool stunned = false;
    public bool bashing = false;
    bool particleHandler = true;

    [SerializeField] private ParticleSystem LightningParticles;

    private ParticleSystem LightningParticlesinstance;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Animator.SetFloat("Xvelocity", Mathf.Abs(EnemyMovement.velocity.x));
        if (stunned)
        {
            EnemyMovement.velocity.x = 0;
        }
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

        if (Detection.Detected && !stunned)
        {
            float distanceToPlayer = (transform.position.x - Player.transform.position.x);
            if (Animator.GetBool("Bashing") == false && canFlip == true)
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

        if(bashing){
            if (particleHandler) {
                SpawnLightningParticles(transform.position.x, transform.position.y); 
                particleHandler = false;}
            else{
                particleHandler = true;
            }
        }

        if (Detection.Detected)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Timer = BashCooldown;
        }

        RaycastHit2D hit;
        if (Detection.Detected && !stunned)
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
                    Animator.SetBool("Bashing", true);
                    canFlip = false;
                }
            }
        }
        

        if (bashing)
        {
            //print(EnemyMovement.velocity.x);
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
                //print("playerhit");
                EnemyMovement.velocity.x = 0;
                bashing = false;
                StartCoroutine(flipTime());
                //Animator.SetBool("Bashing", false);
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
                    //print("CURRENT VEL: " + EnemyMovement.velocity.x);
                    bashing = false;
                    StartCoroutine(flipTime());
                    //Animator.SetBool("Bashing", false);
                    EnemyMovement.velocity.x = 0;
                    //print("RESET1");
                }
            }
            else
            {
                EnemyMovement.velocity.x += airResistince;
                if (EnemyMovement.velocity.x > 0)
                {
                    //print("CURRENT VEL: " + EnemyMovement.velocity.x);
                    bashing = false;
                    StartCoroutine(flipTime());
                    //Animator.SetBool("Bashing", false);
                    EnemyMovement.velocity.x = 0;
                    //print("RESET2");
                }
                
            }
        }


    }
    
    public void stunCheck()
    {
        if (!stunned)
        {
            Animator.SetBool("Stunned", true);
            Animator.SetBool("Bashing", false);
            bashing = false;
            EnemyMovement.velocity.x = 0;
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
        canFlip = true;
        Timer = BashCooldown;
        stunned = false;
        Animator.SetBool("Stunned", false);
    }

    IEnumerator flipTime()
    {
        yield return new WaitForSeconds(0.7f);
        canFlip = true;

    }
    
    public void animationDone()
    {
        StartCoroutine(flipTime());
        //print("done");
        Animator.SetBool("Bashing", false);
    }
    public void Bash()
    {
        if (bashing == false)
        {
            Timer = BashCooldown;
            bashing = true;

            //print("BASHING");
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

    public void SpawnLightningParticles(float a, float b){
        if (bashingRight){
            LightningParticlesinstance = Instantiate(LightningParticles, new Vector3(a, b, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        }else{
            LightningParticlesinstance = Instantiate(LightningParticles, new Vector3(a, b, 0),  Quaternion.Euler(new Vector3(0, 0, -90)));
        }
    }
    
}
