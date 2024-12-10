using System;
using System.Collections;
using System.Collections.Generic;
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
            other.GetComponent<EnemyHealth>().takeDamage(Damage);
            Destroy(gameObject);
        }
    }
}