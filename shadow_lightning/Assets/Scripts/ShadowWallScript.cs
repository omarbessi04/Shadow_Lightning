using System;
using UnityEngine;

public class ShadowWallScript : MonoBehaviour
{
    public void Update()
    {
        if (GameManager.instance.currentStateofPlayer == "Shadow")
        {
            gameObject.layer = 3;
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}
