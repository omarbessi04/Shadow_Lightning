using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDetection))]
public class EnemyMovement : MonoBehaviour
{
    public float waitTime = 3f;
    public float base_speed;
    public float speed;
    private EnemyMovementController controller;
    public bool idle = true;

    public bool shouldmoveWaypoints = true;
    public Transform wayPoint1;
    public Transform wayPoint2;

    public Vector2 wayPointPos1;
    public Vector2 wayPointPos2;

    public bool goingRight;

    public Vector2 direction = new Vector2(1, 0);

    public GameObject sRen;

    public float offset;

    private EnemyDetection enemyDetection;
    public bool waiting = false;
    private Coroutine idleCoroutine; 
    public float Gravity = -20f;
    public Vector2 velocity;

    private GameObject Player = null;

    public bool moveTowardsPlayer = false;
    public bool shouldMove = false;

    public Animator Animator = null;

    public bool flippedRight = true;
    public bool tryggvitest = false;

    public bool noVelocityReset = false;
    
    // Start is called before the first frame update
    void Start()
    {
        base_speed = speed;
        enemyDetection = GetComponent<EnemyDetection>();
        if (goingRight == false)
        {
            direction = new Vector2(direction.x * -1, direction.y);
            
            flipX(true);
        }
        else
        {
            flipX(false);
        }
        wayPointPos1 = wayPoint1.position;
        wayPointPos2 = wayPoint2.position;
        controller = GetComponent<EnemyMovementController>();
    }

    void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }
        if (enemyDetection.Detected)
        {
            speed = base_speed + 1;
        }
        else
        {
            speed = base_speed;
        }
        Animator.SetFloat("xVelocity", MathF.Abs(velocity.x));
        if (enemyDetection.Detected && Player == null)
        {
            Player = GameObject.FindWithTag("PlayerEnemy");
        }
        if (enemyDetection.Detected)
        {
            idle = false;
            
            if (idleCoroutine != null)
            {
                shouldMove = true;
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
                waiting = false;
            }
        }
        if (idle == false && enemyDetection.Detected == false && waiting == false)
        {
            shouldMove = false;
            idleCoroutine = StartCoroutine(idleWait());
        }
        if (idle && shouldmoveWaypoints)
        {

            float distanceToWaypoint1 = Mathf.Abs(transform.position.x - wayPointPos1.x);

            if (goingRight)
            {
                if (distanceToWaypoint1 < 0.1f && controller.collisions.below)
                {
                    goingRight = false;
                    direction = new Vector2(direction.x * -1, direction.y);
                    flipX(true);
                }

                if (controller.collisions.right)
                {
                    goingRight = false;
                    direction = new Vector2(direction.x * -1, direction.y);
                    flipX(true);
                }
            }
            else
            {
                float distanceToWaypoint2 = Mathf.Abs(transform.position.x - wayPointPos2.x);

                if (distanceToWaypoint2 < 0.1f && controller.collisions.below)
                {
                    goingRight = true;
                    direction = new Vector2(direction.x * -1, direction.y);
                    flipX(false);
                }

                if (controller.collisions.left)
                {
                    goingRight = true;
                    direction = new Vector2(direction.x * -1, direction.y);
                    flipX(false); 
                }
            }
            if (controller.collisions.below && shouldMove && !noVelocityReset)
            {
                velocity.x = direction.x * speed;
            }
        }

        else if (moveTowardsPlayer && shouldMove && idle == false && Player != null)
        {
            Vector2 playerDirection = ((Player.transform.position - transform.position).normalized);
            if (controller.collisions.below && !noVelocityReset)
            {
                velocity.x = playerDirection.x * speed;
            }
        }
        
        else if (!shouldMove && !idle && !noVelocityReset || !moveTowardsPlayer && !idle && !noVelocityReset)
        {
            velocity.x = 0;
        }

        if (!controller.collisions.below)
        {
            velocity.y += Gravity * Time.deltaTime;
        }
        controller.Move (velocity * Time.deltaTime);
        
            
    }

    public void knockBack(float knockBack)
    {
        velocity.x = knockBack;
    }
    IEnumerator idleWait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        if (enemyDetection.Detected == false)
        {
            idle = true;
            shouldMove = true;

            if (goingRight)
            {
                flipX(false);
            }
            else
            {
                flipX(true);
            }
        }

        waiting = false;
        idleCoroutine = null; 
    }

    public void flipX(bool value)
    {
        if (value)
        {
            sRen.GetComponent<SpriteRenderer>().flipX = true;
            sRen.transform.localPosition = new Vector3(offset, sRen.transform.localPosition.y,
                sRen.transform.localPosition.z);
            flippedRight = false;


        }
        else
        {
            sRen.GetComponent<SpriteRenderer>().flipX = false;
            sRen.transform.localPosition =
                new Vector3(0, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
            flippedRight = true;

        }
            
    }
}
