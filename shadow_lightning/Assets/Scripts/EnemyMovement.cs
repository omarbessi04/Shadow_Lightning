using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDetection))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public bool idle = true;
    
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
    
    // Start is called before the first frame update
    void Start()
    {
        enemyDetection = GetComponent<EnemyDetection>();
        if (goingRight == false)
        {
            direction = new Vector2(direction.x * -1, direction.y);
            sRen.GetComponent<SpriteRenderer>().flipX = true;
            sRen.transform.localPosition = new Vector3(offset, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
        }
        wayPointPos1 = wayPoint1.position;
        wayPointPos2 = wayPoint2.position;
    }

    void FixedUpdate()
    {
        if (enemyDetection.Detected)
        {
            idle = false;
            
            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
                waiting = false;
            }
        }
        if (idle == false && enemyDetection.Detected == false && waiting == false)
        {
            idleCoroutine = StartCoroutine(idleWait());
        }
        if (idle)
        {

            float distanceToWaypoint1 = Mathf.Abs(transform.position.x - wayPointPos1.x);

            if (goingRight)
            {
                if (distanceToWaypoint1 < 0.1f)
                {
                    goingRight = false;
                    direction = new Vector2(direction.x * -1, direction.y);
                    sRen.GetComponent<SpriteRenderer>().flipX = true;
                    sRen.transform.localPosition = new Vector3(offset, sRen.transform.localPosition.y,
                        sRen.transform.localPosition.z);
                }
            }
            else
            {
                float distanceToWaypoint2 = Mathf.Abs(transform.position.x - wayPointPos2.x);

                if (distanceToWaypoint2 < 0.1f)
                {
                    goingRight = true;
                    direction = new Vector2(direction.x * -1, direction.y);
                    sRen.GetComponent<SpriteRenderer>().flipX = false;
                    sRen.transform.localPosition =
                        new Vector3(0, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
                }
            }

            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    IEnumerator idleWait()
    {
        waiting = true;
        yield return new WaitForSeconds(3f);

        if (enemyDetection.Detected == false)
        {
            idle = true;

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
        }
        else
        {
            sRen.GetComponent<SpriteRenderer>().flipX = false;
            sRen.transform.localPosition =
                new Vector3(0, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
        }
            
    }

}
