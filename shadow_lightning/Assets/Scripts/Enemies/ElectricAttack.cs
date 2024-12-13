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
        if (direction == Vector2.left)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        player = GameObject.FindGameObjectWithTag("PlayerEnemy");
        if (player != null && player.GetComponent<PlayerMovement>().type == "Shield")
        {
            if (Mathf.Abs((player.transform.position.x - transform.position.x)) < 1.5f)
            {
                Destroy(gameObject);
            }
        }
        Destroy(gameObject,10f);
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(behind);
        if (player != null)
        {
            if (direction == Vector2.right && (player.transform.position.x - transform.position.x) < 0)
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
            
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("PlayerEnemy");
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
