using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldProjectileDestroy : MonoBehaviour
{
    public bool right;
    public EnemyMovement EnemyMovement;
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!right && EnemyMovement.flippedRight)
        {
            collider.enabled = false;
        }
        else if (!right && !EnemyMovement.flippedRight)
        {
            collider.enabled = true;
        }
        
        else if (right && !EnemyMovement.flippedRight)
        {
            collider.enabled = false;
        }
        else if (right && EnemyMovement.flippedRight)
        {
            collider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            Destroy(other.GameObject());
        }
    }
}
