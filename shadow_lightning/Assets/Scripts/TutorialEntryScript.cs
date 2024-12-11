using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEntryScript : MonoBehaviour
{
    public string sceneTo;
     void OnTriggerStay2D(Collider2D other) {

        if(other.CompareTag("Player") || other.CompareTag("PlayerEnemy")){
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {

                SceneTransitionScript.instance.TeleportTo(sceneTo);
            }
        }

    }
}
