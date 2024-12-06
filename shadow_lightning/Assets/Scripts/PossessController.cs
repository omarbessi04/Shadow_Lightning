using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PossessController : MonoBehaviour
{
    public bool canPossess = false;
    public Collider2D enemyToPossess = null;
    public Transform Spawner;
    private PlayerController2D controller2D;
    private Animator Animator;
    public bool turningRight;

    public GameObject mage;
    public GameObject sword;
    Camera myCam;
    CameraEffectScript myCamEffects;

	[Header("--- Omar Was Here ---")]
	AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
        myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myCamEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffectScript>();
	}

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canPossess)
        {
            if (!myCamEffects.WorkingOnIt && myCam.orthographicSize == myCamEffects.maxZoom)
            {
                myCamEffects.StartZoom();
            }

            if (Input.GetAxisRaw("Ability") == 1)
            {
                GetComponent<PlayerController2D>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                canPossess = false;
                if (enemyToPossess.GameObject().GetComponent<EnemyMovement>().flippedRight)
                {
                    turningRight = true;
                }
                else
                {
                    turningRight = false;
                }
                GameManager.instance.alive_enemy_count -= 1;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().speed = 0;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().idle = false;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().shouldMove = false;

                Animator.SetBool("Possessing", true);
                
            }
        }
        else
        {
            if(!myCamEffects.WorkingOnIt) checkCamera();
        }
    }

    private void checkCamera(){
        if (myCam.orthographicSize != myCamEffects.maxZoom){
            myCamEffects.StopZoom();
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
                Instantiate(mage, new Vector2(enemyToPossess.transform.position.x-0.184f, enemyToPossess.transform.position.y), Quaternion.identity);
            }
            else
            {
                mage.GetComponentInChildren<PlayerAnimator>().lookingRight = true;
                Instantiate(mage, enemyToPossess.transform.position, Quaternion.identity);
            }
            
        }
        else if (enemyType == "Sword")
        {
            if (turningRight == false)
            {
                sword.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
                Instantiate(sword, new Vector2(enemyToPossess.transform.position.x + 0.85f, enemyToPossess.transform.position.y), Quaternion.identity);
            }
            else
            {
                sword.GetComponentInChildren<PlayerAnimator>().lookingRight = true;
                Instantiate(sword, enemyToPossess.transform.position, Quaternion.identity);
            }
        }
        audioManager.SwitchMusic(audioManager.BattleMusic);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Animator.GetBool("Possessing"))
        {
            if (other.tag == "Enemy")
            {
                canPossess = true;
                if (enemyToPossess == null)
                {
                    enemyToPossess = other;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Animator.GetBool("Possessing")){
            if (other.tag == "Enemy")
            {
                if (enemyToPossess != null)
                {
                    if (enemyToPossess == other)
                    {
                        enemyToPossess = null;
                        canPossess = false;
                    }
                }
                else
                {
                    canPossess = false;
                    enemyToPossess = null;
                }
            }
        }
    }
}
