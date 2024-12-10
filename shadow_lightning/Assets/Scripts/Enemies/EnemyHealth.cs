using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 3f;
    public SpriteRenderer mySprite;
    private EnemyMovement EnemyMovement;
    AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    private void Start()
    {
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    public void takeDamage(float Damage)
    {
        if (GetComponent<EnemyVariables>().typeEnemy == "Mage") audioManager.PlaySFX(audioManager.MageOuch);
        if (GetComponent<EnemyVariables>().typeEnemy == "Sword") audioManager.PlaySFX(audioManager.SwordUgh);
        EnemyMovement.idle = false;
        Health -= Damage;
       StartCoroutine(ChangeColor());
        if (Health <= 0)
        {
            GameManager.instance.alive_enemy_count -= 1;
            Destroy(gameObject);
        }
    
    }

    private IEnumerator ChangeColor(){
        mySprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        mySprite.color = Color.white;
    }
}
