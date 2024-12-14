using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Sources ---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--- Music Clips ---")]
    public AudioClip SneakMusic;
    public AudioClip BattleMusic;

    [Header("--- SFX Clips ---")]
    public AudioClip SwordHit;
    public AudioClip ElectricZap; 
    public AudioClip ShieldDash;
    public AudioClip Jump;
    public AudioClip MageOuch;
    public AudioClip SwordUgh;
    public AudioClip Boom;

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
