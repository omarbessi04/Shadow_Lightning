using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Sources ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- Audio Clips ---")]
    public AudioClip SneakMusic;
    public AudioClip BattleMusic;
    public AudioClip FunnyJumpExample;

    private void Start(){
        musicSource.clip = SneakMusic;
        musicSource.Play();
    }

    public void SwitchMusic(AudioClip clip){
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }


}
