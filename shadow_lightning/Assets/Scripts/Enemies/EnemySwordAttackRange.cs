using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySwordAttackRange : MonoBehaviour
{
   public EnemySwordAttack EnemySwordAttack;
   public bool Left;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "PlayerEnemy")
      {
         EnemySwordAttack.inRange = true;
      }
   }

   void OnTriggerExit2D(Collider2D other)
   {
      if (other.tag == "PlayerEnemy")
      {
         EnemySwordAttack.inRange = false;
      }
   }
}
