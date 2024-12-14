using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PossessController : MonoBehaviour
{
    public float unpossessCooldown = 10f;
    public float Timer;
    public bool canPossess = false;
    public Collider2D enemyToPossess = null;
    public Transform Spawner;
    private PlayerController2D controller2D;
    private Animator Animator;
    public bool turningRight;

    public GameObject mage;
    public GameObject sword;
    public GameObject shield;
    Camera myCam;
    CameraEffectScript myCamEffects;

    public bool possessed = false;
    public GameObject shadow;

    private GameObject possessedEnemyObject;

    [Header("--- Omar Was Here ---")]
    AudioManager audioManager;
    

    private bool currentlyUnpossessing = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
        myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myCamEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffectScript>();
    }

    private void Start()
    {
        shadow = gameObject;
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GameManager.instance.unpossessTimer = Timer;
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }

        if (possessed && !currentlyUnpossessing)
        {
            if (Timer <= 0 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.R)))
            {
                unpossess();
            }
        }

        if (canPossess && possessed == false && GetComponent<PlayerController2D>().collisions.below)
        {
            if (!myCamEffects.WorkingOnIt && myCam.orthographicSize == myCamEffects.maxZoom)
            {
                myCamEffects.StartZoom();
            }

            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.R))
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
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().idle = false;
                enemyToPossess.GameObject().GetComponent<EnemyMovement>().shouldMove = false;

                Animator.SetBool("Possessing", true);

            }
        }
        else
        {
            if (!myCamEffects.WorkingOnIt) checkCamera();
        }
    }

    private void checkCamera()
    {
        if (myCam.orthographicSize != myCamEffects.maxZoom)
        {
            myCamEffects.StopZoom();
        }
    }

    public void possess()
    {
        
        Timer = unpossessCooldown;
        string enemyType = enemyToPossess.GetComponent<EnemyVariables>().typeEnemy;
        enemyToPossess.GameObject().SetActive(false);
        
        if (enemyType == "Mage")
        {
            if (turningRight == false)
            {
                mage.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
                possessedEnemyObject = Instantiate(mage, new Vector2(enemyToPossess.transform.position.x - 0.184f, enemyToPossess.transform.position.y), Quaternion.identity);
            }
            else
            {
                mage.GetComponentInChildren<PlayerAnimator>().lookingRight = true;
                possessedEnemyObject = Instantiate(mage, enemyToPossess.transform.position, Quaternion.identity);
            }

        }
        else if (enemyType == "Sword")
        {
            if (turningRight == false)
            {
                sword.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
                possessedEnemyObject = Instantiate(sword, new Vector2(enemyToPossess.transform.position.x + 0.85f, transform.position.y+0.5f), Quaternion.identity);
            }
            else
            {
                sword.GetComponentInChildren<PlayerAnimator>().lookingRight = true;
                possessedEnemyObject = Instantiate(sword, new Vector2(enemyToPossess.transform.position.x, transform.position.y+0.5f), Quaternion.identity);
            }
        }
        else if (enemyType == "Shield")
        {
            if (turningRight == false)
            {
                shield.GetComponentInChildren<PlayerAnimator>().lookingRight = false;
                possessedEnemyObject = Instantiate(shield, enemyToPossess.transform.position, Quaternion.identity);
            }
            else
            {
                shield.GetComponentInChildren<PlayerAnimator>().lookingRight = true;
                possessedEnemyObject = Instantiate(shield, enemyToPossess.transform.position, Quaternion.identity);
            }
        }
        audioManager.SwitchMusic(audioManager.BattleMusic);
        possessed = true;
        shadow.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    void possessAnimationDone()
    {
        Animator.SetBool("Possessing", false);

    }
    void unpossess()
    {
        currentlyUnpossessing = true;
        possessedEnemyObject.tag = "Untagged";
        possessedEnemyObject.layer = 0;
        GameObject currentEnemy = enemyToPossess.GameObject();
        GetComponent<PlayerController2D>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("CameraAnchor").GetComponent<CameraFollow>().target = GetComponent<PlayerController2D>();

        currentEnemy.transform.position = possessedEnemyObject.transform.position;


        if (possessedEnemyObject.GetComponentInChildren<PlayerAnimator>().lookingRight)
        {
            currentEnemy.GetComponent<EnemyMovement>().flipX(false);
            currentEnemy.GetComponent<EnemyMovement>().direction = Vector2.right;
            currentEnemy.GetComponent<EnemyMovement>().goingRight = true;
        }
        else
        {
            currentEnemy.GetComponent<EnemyMovement>().flipX(true);
            currentEnemy.GetComponent<EnemyMovement>().direction = Vector2.left;
            currentEnemy.GetComponent<EnemyMovement>().goingRight = false;
        }

        currentEnemy.GetComponent<EnemyMovement>().shouldMove = true;
        currentEnemy.GetComponent<EnemyMovement>().noVelocityReset = true;
        StartCoroutine(ReenableMovement(currentEnemy));
        currentEnemy.GetComponent<EnemyMovement>().velocity = possessedEnemyObject.GetComponent<PlayerMovement>().velocity;
        currentEnemy.SetActive(true);
        if (possessedEnemyObject.GetComponent<PlayerController2D>().collisions.right)
        {
            print("going left");
            shadow.transform.position = new Vector2(possessedEnemyObject.transform.position.x-2f, possessedEnemyObject.transform.position.y);
        }
        else if (possessedEnemyObject.GetComponent<PlayerController2D>().collisions.left)
        {
            print("going right");
            shadow.transform.position = new Vector2(possessedEnemyObject.transform.position.x+2f, possessedEnemyObject.transform.position.y);
        }
        else
        {
            shadow.transform.position = possessedEnemyObject.transform.position;
        }

        shadow.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void FixZForPossess(){
        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, 1);
    }

    private IEnumerator ReenableMovement(GameObject enemy)
    {
        yield return new WaitForSeconds(0.05f);
        enemy.GetComponent<EnemyMovement>().noVelocityReset = false;
        enemy.GetComponent<EnemyMovement>().idle = true;
        if (!possessedEnemyObject.GetComponent<PlayerController2D>().collisions.below && !enemy.GetComponent<EnemyMovementController>().collisions.right && !enemy.GetComponent<EnemyMovementController>().collisions.left)
        {
            GetComponent<PlayerMovement>().velocity.y += 5f;
            if (enemy.GetComponent<EnemyMovement>().flippedRight)
            {
                GetComponent<PlayerMovement>().velocity.x += 20f;
            }
            else
            {
                GetComponent<PlayerMovement>().velocity.x -= 20f;
            }
        }
        else
        {
            GetComponent<PlayerMovement>().velocity = Vector3.zero;
        }
        
        
        currentlyUnpossessing = false;
        Destroy(possessedEnemyObject);
        possessed = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Animator.GetBool("Possessing"))
        {
            if (other.tag == "Enemy" && !other.GameObject().GetComponent<EnemyVariables>().boss)
            {
                canPossess = true;
                if (enemyToPossess == null)
                {
                    enemyToPossess = other;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!Animator.GetBool("Possessing"))
        {
            if (other.tag == "Enemy" && !other.GameObject().GetComponent<EnemyVariables>().boss)
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
        if (!Animator.GetBool("Possessing"))
        {
            if (other.tag == "Enemy" && !other.GameObject().GetComponent<EnemyVariables>().boss)
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