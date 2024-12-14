using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class unpossessCooldown : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text; 
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

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
