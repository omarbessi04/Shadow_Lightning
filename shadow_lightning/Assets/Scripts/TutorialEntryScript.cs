using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEntryScript : MonoBehaviour
{
    public string sceneTo;
    private void OnTriggerStay2D(Collider2D other) {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {

            SceneTransitionScript.instance.TeleportTo(sceneTo);
        }
    }
}
