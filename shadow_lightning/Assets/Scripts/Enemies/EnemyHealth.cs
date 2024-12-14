using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool boss = false;
    public float Health = 3f;
    public SpriteRenderer mySprite;
    private EnemyMovement enemyMovement;
    private BossMovement BossMovement;
    AudioManager audioManager;

    [SerializeField] private ParticleSystem DamageParticles;

    private ParticleSystem DamageParticlesinstance;
    GameObject player_for_pos;
    Transform playerPos;

    public GameObject wallJumpUnlock;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    private void Start()
    {
        if (!boss)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        else
        {
            BossMovement = GetComponent<BossMovement>();
        }
    }

    public void takeDamage(float Damage, string type = "")
    {
        string enemyType = GetComponent<EnemyVariables>().typeEnemy;
        if (enemyType == "Mage") audioManager.PlaySFX(audioManager.MageOuch);
        if (enemyType == "Sword") audioManager.PlaySFX(audioManager.SwordUgh);
        if (enemyType == "Shield") audioManager.PlaySFX(audioManager.ShieldOof);

        if (enemyType == "Shield" && type == "Sword")
        {
            if (GetComponent<ShieldBash>().Animator.GetBool("Bashing"))
            {
                GetComponent<EnemyMovement>().velocity.x = 0;
                GetComponent<ShieldBash>().stunCheck();
            }
        }

        SpawnDamageParticles(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);

        if (!boss)
        {
            enemyMovement.idle = false;
        }

        Health -= Damage;
       StartCoroutine(ChangeColor());

        if (Health <= 0)
        {
            if (GetComponent<EnemyVariables>().boss){
                wallJumpUnlock.GetComponent<WallJumpPopUpScript>().Unlock();
                GameManager.instance.BossHasBeenKilled = true;
            };
            GameManager.instance.alive_enemy_count -= 1;
            GetComponent<Animator>().SetBool("dying", true);
        }
    }

    public void death()
    {
        Destroy(gameObject);
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
