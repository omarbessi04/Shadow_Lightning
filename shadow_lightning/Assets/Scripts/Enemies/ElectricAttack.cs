using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricAttack : MonoBehaviour
{

    public Vector2 direction;
    public float Damage = 0.5f;

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
        if (other.tag == "PlayerEnemy")
        {
            if (other.GameObject().GetComponent<PlayerMovement>().type == "Shield")
            {
                if (direction == Vector2.right && !other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight)
                {
                    Destroy(gameObject);
                }
                else if (direction == Vector2.left && other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight)
                {
                    Destroy(gameObject);
                }
                else
                {
                    GameManager.instance.heartSystem.TakeDamage(Damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                GameManager.instance.heartSystem.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}
