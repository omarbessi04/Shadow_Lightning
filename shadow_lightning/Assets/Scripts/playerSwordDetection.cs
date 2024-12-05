using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerSwordDetection : MonoBehaviour
{
    public bool Right;
    public PlayerAnimator PlayerAnimator;
    public PlayerSwordAttack PlayerSwordAttack;
    private EdgeCollider2D collider;


    private void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (Right)
        {
            if (PlayerAnimator.lookingRight && PlayerSwordAttack.Animator.GetBool("Attacking"))
            {
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
        }
        else
        {
            if (!PlayerAnimator.lookingRight && PlayerSwordAttack.Animator.GetBool("Attacking"))
            {
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!PlayerSwordAttack.enemiesHit.Contains(other.gameObject))
            {
                PlayerSwordAttack.enemiesHit.Add(other.gameObject);
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!PlayerSwordAttack.enemiesHit.Contains(other.gameObject))
            {
                PlayerSwordAttack.enemiesHit.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (PlayerSwordAttack.enemiesHit.Contains(other.gameObject))
            {
                PlayerSwordAttack.enemiesHit.Remove(other.gameObject);
            }
        }
    }

    }
