using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PossessController : MonoBehaviour
{
    public bool canPossess = false;
    public Collider2D enemyToPossess;
    public Transform Spawner;
    private PlayerController2D controller2D;
    private Animator Animator;
    private bool turningRight;

    public GameObject mage;
    public GameObject sword;

	[Header("--- Omar Was Here ---")]
	AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canPossess)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameManager.instance.alive_enemy_count -= 1;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().speed = 0;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().idle = false;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().shouldMove = false;

                if (enemyToPossess.GameObject().GetComponent<EnemyMovement>().flippedRight)
                {
                    turningRight = true;
                }
                else
                {
                    turningRight = false;
                }
                Animator.SetBool("Possessing", true);
                
            }
        }
    }
    
    public void possess()
    {
        string enemyType = enemyToPossess.GetComponent<EnemyVariables>().typeEnemy;
        enemyToPossess.GameObject().SetActive(false);

        if (enemyType == "Mage")
        {
            if (turningRight == false)
            {
                mage.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
            }

            Instantiate(mage, Spawner.position, Quaternion.identity);
        }
        else if (enemyType == "Sword")
        {
            if (turningRight == false)
            {
                sword.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
            }
            Instantiate(sword, Spawner.position, Quaternion.identity);
        }
        audioManager.SwitchMusic(audioManager.BattleMusic);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            canPossess = true;
            enemyToPossess = other;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            canPossess = false;
            enemyToPossess = null;
        }
    }
}
