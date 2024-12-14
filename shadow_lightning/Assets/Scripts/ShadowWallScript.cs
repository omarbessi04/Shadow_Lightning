using System;
using UnityEngine;

public class ShadowWallScript : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask normal;
    public void Update()
    {
        if (GameManager.instance.currentStateofPlayer == "Shadow")
        {
            gameObject.layer = ground;
        }
        else
        {
            gameObject.layer = normal;
        }
    }
}
