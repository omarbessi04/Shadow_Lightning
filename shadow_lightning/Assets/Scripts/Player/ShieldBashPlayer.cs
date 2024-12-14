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
    bool particleHandler = true;
    AudioManager audioManager;
    [SerializeField] private ParticleSystem LightningParticles;

    private ParticleSystem LightningParticlesinstance;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Animator.SetFloat("xVelocity", Mathf.Abs(Movement.velocity.x));
        if (stunned)
        {
            Movement.velocity.x = 0;
        }
        Timer -= Time.deltaTime;
        if (!bashing && Timer < 0 && Input.GetAxisRaw("Ability") == 1)
        {
            Animator.SetBool("Bashing", true);
            audioManager.PlaySFX(audioManager.ShieldDash);
        }

        if (bashing)
        {
            if (particleHandler)
            {
                SpawnLightningParticles(transform.position.x, transform.position.y);
                particleHandler = false;
            }
            else
            {
                particleHandler = true;
            }
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
            if (!enemy.GetComponent<EnemyVariables>().boss)
            {
                enemy.GetComponent<EnemyMovement>().knockBack(knockBack);
            }

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
            particleHandler = true;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boulder" && bashing)
        {
            BoulderShake boulderShake = other.GameObject().GetComponent<BoulderShake>();

            boulderShake.Begin();
        }
    }

    public void SpawnLightningParticles(float a, float b)
    {
        if (bashingRight)
        {
            LightningParticlesinstance = Instantiate(LightningParticles, new Vector3(a, b, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        }
        else
        {
            LightningParticlesinstance = Instantiate(LightningParticles, new Vector3(a, b, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        }
    }

}

