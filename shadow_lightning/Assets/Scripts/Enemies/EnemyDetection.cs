using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyDetection : MonoBehaviour
{
   public bool Detected = false;
   public EnemyMovement enemyMovement;
   public float detectionRange = 100f;
   public LayerMask mask;

   private void Update()
   {
      RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, enemyMovement.direction, detectionRange, mask);
      RaycastHit2D hitOther = Physics2D.Raycast(transform.localPosition, new Vector2(enemyMovement.direction.x*-1, enemyMovement.direction.y), detectionRange, mask);
      if (hit.collider != null)
      {
         Detected = true;
      }

      else if (enemyMovement.idle == false && hitOther.collider != null)
      {
         Detected = true;
      }
      else
      {
         Detected = false;
      }
   }
}
