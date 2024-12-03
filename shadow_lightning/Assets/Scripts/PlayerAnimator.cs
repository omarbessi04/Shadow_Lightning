using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   public PlayerMovement playerMovement;
   public SpriteRenderer sRen;
   public float offset;

   public bool lookingRight = true;
    

   void Update()
   {
      if (lookingRight && playerMovement.velocity.normalized.x < 0)
      {
         sRen.flipX = true;
         lookingRight = false;
         if (offset != 0)
         {
            transform.localPosition = new Vector3(offset, transform.localPosition.y, transform.localPosition.z);
         }
      }
      else if (lookingRight == false && playerMovement.velocity.normalized.x > 0)
      {
         sRen.flipX = false;
         lookingRight = true;
         if (offset != 0)
         {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
         }
      }
      
   }
}
