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

    public GameObject mage;
    public GameObject sword;

	[Header("--- Omar Was Here ---")]
	AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    private void Start()
    {
    }

    private void Update()
    {
        if (canPossess)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                possess();
            }
        }
    }

    void possess()
    {
        string enemyType = enemyToPossess.GetComponent<EnemyVariables>().typeEnemy;
        enemyToPossess.GameObject().SetActive(false);

        if (enemyType == "Mage")
        {
            Instantiate(mage, Spawner.position, Quaternion.identity);
        }
        else if (enemyType == "Sword")
        {
            Instantiate(sword, Spawner.position, Quaternion.identity);
        }
        audioManager.SwitchMusic(audioManager.BattleMusic);

        gameObject.active = false;
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
