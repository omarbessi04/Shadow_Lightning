using UnityEngine;

public class LoverScript : MonoBehaviour
{
    [SerializeField] private GameObject LoveParticleGameObject;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
        LoveParticleGameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoveParticleGameObject.SetActive(true);
        }
    }
}
