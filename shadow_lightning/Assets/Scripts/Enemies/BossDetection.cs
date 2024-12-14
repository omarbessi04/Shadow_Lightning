using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BossDetection : MonoBehaviour
{
    public bool Detected = false;
    public BossMovement enemyMovement;
    public float detectionRange = 100f;
    public LayerMask mask;
    public Transform marker2D;

    public Transform jumpTransform;

    private void Update()
    {
      
        RaycastHit2D hit = Physics2D.Raycast(marker2D.transform.position, enemyMovement.direction, detectionRange, mask);
        RaycastHit2D hitJump = Physics2D.Raycast(jumpTransform.position, enemyMovement.direction, detectionRange, mask);
        RaycastHit2D hitOther = Physics2D.Raycast(marker2D.transform.position, new Vector2(enemyMovement.direction.x*-1, enemyMovement.direction.y), detectionRange, mask);
        RaycastHit2D hitOtherJump = Physics2D.Raycast(jumpTransform.position, new Vector2(enemyMovement.direction.x*-1, enemyMovement.direction.y), detectionRange, mask);
        if (!Detected && hit.collider != null || (hitJump.collider != null && !GameManager.instance.currentPlayer.GetComponent<PlayerController2D>().collisions.below))
        {
            Detected = true;
            KillAllHumans();
            
        }

        else if (!Detected && enemyMovement.idle == false && (hitOther.collider != null || (hitOtherJump.collider != null && !GameManager.instance.currentPlayer.GetComponent<PlayerController2D>().collisions.below)))
        {
            Detected = true;
            KillAllHumans();
        }
    }

    void KillAllHumans()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i];

            if (enemy != gameObject)
            {
                enemy.GetComponent<EnemyMovement>().Animator.SetBool("dying", true);
            }
        }
    }
}