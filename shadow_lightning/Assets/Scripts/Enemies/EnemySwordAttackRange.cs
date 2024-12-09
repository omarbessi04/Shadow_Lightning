using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySwordAttackRange : MonoBehaviour
{
   public EnemySwordAttack EnemySwordAttack;
   public EnemyMovement EnemyMovement;
   public bool Left;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "PlayerEnemy")
      {
         EnemySwordAttack.inRange = true;
         EnemyMovement.moveTowardsPlayer = false;
      }
   }

   void OnTriggerExit2D(Collider2D other)
   {
      if (other.tag == "PlayerEnemy")
      {
         EnemySwordAttack.inRange = false;
         EnemyMovement.moveTowardsPlayer = true;
      }
   }
}
