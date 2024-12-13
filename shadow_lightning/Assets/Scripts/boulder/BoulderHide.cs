using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderHide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject boulder = GameObject.FindGameObjectWithTag("Boulder");

        if(boulder) boulder.transform.parent.gameObject.SetActive(false);
    }
}
