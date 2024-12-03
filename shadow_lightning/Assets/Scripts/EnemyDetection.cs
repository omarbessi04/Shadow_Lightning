using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
   public bool Detected = false;
   public EnemyMovement enemyMovement;
   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "PlayerEnemy")
      {
         Detected = true;
         enemyMovement.idle = false;
      }
   }
}
