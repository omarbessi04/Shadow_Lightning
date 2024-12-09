using UnityEngine;

public class BoulderDestroyerScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem boulderParticles;

    private ParticleSystem boulderParticlesinstance;

    public void SpawnBoulderParticles(float a, float b, float c){
        boulderParticlesinstance = Instantiate(boulderParticles, new Vector3(a, b, c), Quaternion.identity);
    }
}
