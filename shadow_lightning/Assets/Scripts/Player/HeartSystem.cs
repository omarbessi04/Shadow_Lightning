using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Rendering;
// using UnityEngine.UIElements;

public class HeartSystem : MonoBehaviour
{
    public Image[] Hearts;
    public float Health = 3;
    public Material heartMaterial;
    public float heartAnimationSpeed;
    private bool isColorChanging = false;
    private void Start()
    {
        UpdateHealthBar();
        GameManager.instance.heartSystem = this;
        heartMaterial.color = Color.white;
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        UpdateHealthBar();
        if (!isColorChanging)
        {
            StartCoroutine(AnimateHealthbar());
        }
        if (Health <= 0)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
        SceneTransitionScript.instance.TeleportTo("GameOver");
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
}
