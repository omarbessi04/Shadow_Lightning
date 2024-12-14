using System;
using UnityEngine;

public class UnpossessCooldown : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text; 
    // Update is called once per frame
    void Update()
    {
        if (MathF.Round(GameManager.instance.unpossessTimer) <= 0)
        {
            text.text = "";
        }
        else
        {
            text.text = "Time until you can unpossess: " + MathF.Round(GameManager.instance.unpossessTimer).ToString();
        }
    }
}
