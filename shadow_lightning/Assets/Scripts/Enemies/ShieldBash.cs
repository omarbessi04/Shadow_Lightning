using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    private EnemyMovement EnemyMovement;
    public float bashRange = 5f;
    public float minBashRange = 3f;
    public LayerMask mask;
    public float bashSpeed = 40f;
    void start()
    {
        EnemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, EnemyMovement.direction, bashRange, mask);
        if (hit && hit.distance > minBashRange)
        {
            Bash();
        }
    }

    public void Bash()
    {
        EnemyMovement.velocity.x += bashSpeed;
        GetComponent<EnemyMovementController>().Move(EnemyMovement.velocity*Time.deltaTime);
    }
}
