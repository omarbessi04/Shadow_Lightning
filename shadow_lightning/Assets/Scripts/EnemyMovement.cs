using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    
    public Transform wayPoint1;
    public Transform wayPoint2;

    public Vector2 wayPointPos1;
    public Vector2 wayPointPos2;

    private bool goingRight;

    public Vector2 direction = new Vector2(1, 0);

    public GameObject sRen;

    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        wayPointPos1 = wayPoint1.position;
        wayPointPos2 = wayPoint2.position;
    }
    void FixedUpdate()
    {

        float distanceToWaypoint1 = Mathf.Abs(transform.position.x - wayPointPos1.x);

        if (goingRight)
        {
            if (distanceToWaypoint1 < 0.1f) 
            {
                goingRight = false;
                speed = speed * -1;
                sRen.GetComponent<SpriteRenderer>().flipX = false;
                sRen.transform.localPosition = new Vector3(0, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
            }
        }
        else
        {
            float distanceToWaypoint2 = Mathf.Abs(transform.position.x - wayPointPos2.x);

            if (distanceToWaypoint2 < 0.1f) 
            {
                goingRight = true;
                speed = speed * -1;
                sRen.GetComponent<SpriteRenderer>().flipX = true;
                sRen.transform.localPosition = new Vector3(offset, sRen.transform.localPosition.y, sRen.transform.localPosition.z);
            }
        }
        
        transform.Translate(direction * Time.deltaTime * speed);
    }

}
