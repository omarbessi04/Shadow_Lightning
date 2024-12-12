using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectricAttack : MonoBehaviour
{

    public Vector2 direction;
    public float Damage = 0.5f;
    private Collider2D collider;
    private GameObject player;

    public float speed = 5f;

    private bool behind;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerEnemy");
        Destroy(gameObject,10f);
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector2.right &&(player.transform.position.x - transform.position.x) < 0)
        {
            behind = true;
        }
        else if (direction == Vector2.left && (player.transform.position.x - transform.position.x) > 0)
        {
            behind = true;
        }
        else
        {
            behind = false;
        }
       transform.Translate(direction*speed*Time.deltaTime); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().name == "GroundTiles")
        {
            Destroy(gameObject);
        }
        if (other.tag == "PlayerEnemy"){
        
            if (other.GameObject().GetComponent<PlayerMovement>().type == "Shield")
            {
                if (direction == Vector2.right && !other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight && !behind)
                {
                    Destroy(gameObject);
                }
                else if (direction == Vector2.left && other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight && !behind)
                {
                    Destroy(gameObject);
                }
                else if (direction == Vector2.left &&
                         !other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight && behind)
                {
                    Destroy(gameObject);
                }
                else if (direction == Vector2.right && other.GameObject().GetComponentInChildren<PlayerAnimator>().lookingRight && behind)
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
