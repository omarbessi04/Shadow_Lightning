using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   public PlayerMovement playerMovement;
   public SpriteRenderer sRen;
   public float offset;

   public bool lookingRight = true;


   private void Start()
   {
      if (lookingRight == false)
      {
         sRen.flipX = true;
         if (offset != 0)
         {
            transform.localPosition = new Vector3(offset, transform.localPosition.y, transform.localPosition.z);
         }
      }
   }

   void Update()
   {
      if (lookingRight && playerMovement.velocity.normalized.x < 0 && playerMovement.canMove)
      {
         sRen.flipX = true;
         lookingRight = false;
         if (offset != 0)
         {
            transform.localPosition = new Vector3(offset, transform.localPosition.y, transform.localPosition.z);
         }
      }
      else if (lookingRight == false && playerMovement.velocity.normalized.x > 0 && playerMovement.canMove)
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
