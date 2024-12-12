using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    public Vector2 direction;
    public float Damage = 1.5f;

    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GameObject().GetComponent<EnemyVariables>().typeEnemy == "Shield")
            {
                if (direction == Vector2.right && !other.GameObject().GetComponent<EnemyMovement>().flippedRight)
                {
                    Destroy(gameObject);
                }
                else if (direction == Vector2.left && other.GameObject().GetComponent<EnemyMovement>().flippedRight)
                {
                    Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<EnemyHealth>().takeDamage(Damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                other.GetComponent<EnemyHealth>().takeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}