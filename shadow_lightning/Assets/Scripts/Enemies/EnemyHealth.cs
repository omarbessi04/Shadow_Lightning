using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 3f;
    private EnemyMovement EnemyMovement;

    private void Start()
    {
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    public void takeDamage(float Damage)
    {
        EnemyMovement.idle = false;
        Health -= Damage;
        if (Health <= 0)
        {
            GameManager.instance.alive_enemy_count -= 1;
            Destroy(gameObject);
        }
    }
}
