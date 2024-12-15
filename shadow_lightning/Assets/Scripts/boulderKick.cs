using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class boulderKick : MonoBehaviour
{
   public Vector2 wantedPosition;
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player" || other.tag == "PlayerEnemy")
      {
         if (!GameManager.instance.BoulderHasBeenDestroyed)
         {
            other.transform.position = wantedPosition;
         }
      }
   }
}
