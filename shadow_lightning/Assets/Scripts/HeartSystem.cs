using UnityEngine.UI;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public Image[] Hearts;
    public float Health = 3;

    private void Start()
    {
        UpdateHealthBar();
        GameManager.instance.heartSystem = this;
    }
    

    public void TakeDamage(float damage)
    {
        Health -= damage;
        UpdateHealthBar();
        if (Health <= 0){
            SceneTransitionScript.instance.TeleportTo("GameOver");
        }
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
}
