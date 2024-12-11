using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 3f;
    public SpriteRenderer mySprite;
    private EnemyMovement EnemyMovement;
    AudioManager audioManager;

    [SerializeField] private ParticleSystem DamageParticles;

    private ParticleSystem DamageParticlesinstance;
    Transform playerPos;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    private void Start()
    {
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    public void takeDamage(float Damage, string type = "")
    {
        string enemyType = GetComponent<EnemyVariables>().typeEnemy;
        if (enemyType == "Mage") audioManager.PlaySFX(audioManager.MageOuch);
        if (enemyType == "Sword") audioManager.PlaySFX(audioManager.SwordUgh);

        if (enemyType == "Shield" && type == "Sword")
        {
            GetComponent<EnemyMovement>().velocity.x = 0;
            GetComponent<ShieldBash>().stunCheck();
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
        audioManager.PlaySFX(audioManager.Boom);

        float enemyXpos = GetComponent<Transform>().position.x;
        float playerXpos = playerPos.position.x;

        Debug.Log($"PlayerX: {playerXpos}, EnemyX: {enemyXpos}");

        if(playerXpos > enemyXpos){
            Debug.Log("Flipping Particles");
            DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), new Quaternion(0, 180, 0, 0));
        }else{
            Debug.Log("Normal Particles");
            DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.identity);
        }
        StartCoroutine(deleteParticles());
    }

    public IEnumerator deleteParticles(){
        yield return new WaitForSeconds(0.1f);
        Destroy(DamageParticles);
    }
}
