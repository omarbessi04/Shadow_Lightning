using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text; 
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentStateofPlayer == "Shadow")
        {
            text.text = "R or J to possess enemies when behind them.";
        }
        else
        {
            if (GameManager.instance.unpossessTimer >= 0)
            {
                text.text = "E or K to use abilities.";
            }
            else
            {
                text.text = "E or K to use abilities.\nR or J to unpossess.";
            }
        }
    }
}
