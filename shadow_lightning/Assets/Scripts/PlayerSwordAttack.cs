using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    public float damage = 3f;
    public PlayerAnimator playerAnimator;
    public float cooldown = 3f;
    public Transform markerRight;
    public Transform markerLeft;
    public List<GameObject> enemiesHit = new List<GameObject>();
    private float timer;
    public Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Animator.GetBool("Attacking") == false && timer < 0)
            {
                Animator.SetBool("Attacking", true);
            }
        }
    }

    void AttackDone()
    {
        timer = cooldown;
        Animator.SetBool("Attacking", false);
        enemiesHit.Clear();
    }

    void Attack()
    {
        List<GameObject> currEnemiesHit = new List<GameObject>(enemiesHit);
        for (int i = 0; i < currEnemiesHit.Count; i++)
        {
            GameObject enemy = currEnemiesHit[i];
            if (enemy != null)
            {
                enemy.GetComponent<EnemyHealth>().takeDamage(damage);
            }
        }
    }
}

