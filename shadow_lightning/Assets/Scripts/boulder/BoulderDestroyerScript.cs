using UnityEngine;

public class BoulderDestroyerScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem boulderParticles;

    private ParticleSystem boulderParticlesinstance;

    AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}

    public void SpawnBoulderParticles(float a, float b, float c){
        audioManager.PlaySFX(audioManager.Boom);
        GameManager.instance.BoulderHasBeenDestroyed = true;
        boulderParticlesinstance = Instantiate(boulderParticles, new Vector3(a, b, c), Quaternion.identity);
    }
}
