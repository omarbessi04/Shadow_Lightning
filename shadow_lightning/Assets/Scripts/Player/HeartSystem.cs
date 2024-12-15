using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HeartSystem : MonoBehaviour
{
    public Image[] Hearts;
    public float Health = 3;
    public Material heartMaterial;
    public float heartAnimationSpeed;
    private bool isColorChanging = false;
    [SerializeField] private ParticleSystem DamageParticles;
    private ParticleSystem DamageParticlesinstance;
    CameraEffectScript myCamEffects;
    AudioManager audioManager;
    private void Start()
    {
        UpdateHealthBar();
        GameManager.instance.heartSystem = this;
        heartMaterial.color = Color.white;
    }

    private void Awake() {
        myCamEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffectScript>();
        audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
    }


    public void TakeDamage(float damage)
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerEnemy");
        if (player){
            SpawnDamageParticles(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        audioManager.PlaySFX(audioManager.ShadowSwoosh);

        Health -= damage;
        UpdateHealthBar();

        //Still alive, screenshake
        if (Health > 0) myCamEffects.TriggerShake(0.2f,damage);

        //Change heart color
        if (!isColorChanging) StartCoroutine(AnimateHealthbar());

        //Dead, die
        if (Health <= 0) StartCoroutine(PlayerDeath());
    }

    private IEnumerator PlayerDeath()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
        SceneTransitionScript.instance.Restart();
    }

    public void UpdateHealthBar()
    {
        float healthToFill = Health;
        int currHeart = 0;
        foreach (var heart in Hearts)
        {
            heart.fillAmount = 0f;
        }
        while (healthToFill > 0)
        {
            if (healthToFill > 0.5)
            {
                Hearts[currHeart].fillAmount = 1f;
                healthToFill -= 1;
            }
            else
            {
                Hearts[currHeart].fillAmount = 0.5f;
                healthToFill -= 0.5f;
            }

            currHeart += 1;
        }
    }
    private IEnumerator AnimateHealthbar()
    {
        isColorChanging = true;
        float tick = 0f;
        Color heartcolor = Color.white;
        while (heartMaterial.color != Color.red)
        {
            tick += Time.deltaTime * heartAnimationSpeed;
            heartMaterial.color = Color.Lerp(heartcolor, Color.red, tick);
            yield return null;
        }
        tick = 0f;
        while (heartMaterial.color != Color.white)
        {
            tick += Time.deltaTime * heartAnimationSpeed;
            heartMaterial.color = Color.Lerp(Color.red, heartcolor, tick);
            yield return null;
        }
        heartMaterial.color = Color.white;
        isColorChanging = false;
    }

    public void SpawnDamageParticles(float a, float b, float c){
        GameObject player = GameObject.FindGameObjectWithTag("PlayerEnemy");
        if (!player) return;
        
        PlayerAnimator pa = GetComponent<PlayerAnimator>();

        if(pa){
            if(pa.lookingRight){
                DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.Euler(0, 180, 0));
            }else{
                DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.identity);
            }
        }else{
            DamageParticlesinstance = Instantiate(DamageParticles, new Vector3(a, b, c), Quaternion.identity);
        }
    }
}
