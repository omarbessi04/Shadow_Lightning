using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float Health = 20f;
    public SpriteRenderer mySprite;
    private BossMovement EnemyMovement;
    AudioManager audioManager;

    [SerializeField] private ParticleSystem DamageParticles;

    private ParticleSystem DamageParticlesinstance;
    GameObject player_for_pos;
    Transform playerPos;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    private void Start()
    {
        EnemyMovement = GetComponent<BossMovement>();
    }

    public void takeDamage(float Damage, string type = "")
    {
        string enemyType = GetComponent<EnemyVariables>().typeEnemy;
        if (enemyType == "Mage") audioManager.PlaySFX(audioManager.MageOuch);
        if (enemyType == "Sword") audioManager.PlaySFX(audioManager.SwordUgh);

        if (enemyType == "Shield" && type == "Sword")
        {
            if (GetComponent<ShieldBash>().Animator.GetBool("Bashing"))
            {
                GetComponent<EnemyMovement>().velocity.x = 0;
                GetComponent<ShieldBash>().stunCheck();
            }
        }

        SpawnDamageParticles(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);

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

    public void SpawnDamageParticles(float a, float b, float c){

        player_for_pos = GameObject.FindGameObjectWithTag("PlayerEnemy");
        if (!playerPos) player_for_pos = GameObject.FindGameObjectWithTag("Player");

        // If the player unpossesses on the frame that damage is dealt, player_for_pos will be null
        if(player_for_pos){
            playerPos = player_for_pos.GetComponent<Transform>();

            float enemyXpos = GetComponent<Transform>().position.x;
            float playerXpos = playerPos.position.x;

            // Make sure particles spawn facing the right direction.
            if(playerXpos > enemyXpos){
                DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), new Quaternion(0, 180, 0, 0));
            }else{
                DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.identity);
            }
        }else{
            // Just spawn them if there is no player
            DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.identity);
        }
    }
}
