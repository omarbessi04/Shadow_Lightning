using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoverScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem LoveParticles;

    private ParticleSystem LoveParticlesinstance;

    AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    public void SpawnLoveParticles(){
        LoveParticlesinstance = Instantiate(LoveParticles, new Vector3(-17.43303f, 11.9f, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            SpawnLoveParticles();
        }
    }
      
}
