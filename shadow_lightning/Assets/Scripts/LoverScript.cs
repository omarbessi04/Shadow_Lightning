using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoverScript : MonoBehaviour
{
    [SerializeField] private GameObject LoveParticleGameObject;
    private AudioManager audioManager;

    public float gameEndTimer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
        LoveParticleGameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame(){
        LoveParticleGameObject.SetActive(true);
        yield return new WaitForSeconds(gameEndTimer);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
        GameManager.instance.BoulderHasBeenDestroyed = false;
        GameManager.instance.PlayerHasWallJump = false;
        SceneTransitionScript.instance.TeleportTo("MainMenu");
    }
}
