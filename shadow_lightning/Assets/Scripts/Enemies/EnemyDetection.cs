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

   public Transform jumpTransform;

   private void Update()
   {
      if (Detected)
      {
         
      }
      RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyMovement.direction, detectionRange, mask);
      RaycastHit2D hitJump = Physics2D.Raycast(jumpTransform.position, enemyMovement.direction, detectionRange, mask);
      RaycastHit2D hitOther = Physics2D.Raycast(transform.position, new Vector2(enemyMovement.direction.x*-1, enemyMovement.direction.y), detectionRange, mask);
      RaycastHit2D hitOtherJump = Physics2D.Raycast(jumpTransform.position, new Vector2(enemyMovement.direction.x*-1, enemyMovement.direction.y), detectionRange, mask);
      if (hit.collider != null || hitJump.collider != null)
      {
         Detected = true;
      }

      else if (enemyMovement.idle == false && (hitOther.collider != null || hitOtherJump.collider != null))
      {
         Detected = true;
      }
      else
      {
         Detected = false;
      }
   }
}
