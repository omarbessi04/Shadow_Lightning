using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMageAttack : MonoBehaviour
{
    public GameObject mageAttack;
    public PlayerAnimator playerAnimator;
    public float cooldown = 3f;
    public Transform markerRight;
    public Transform markerLeft;
    private float timer;
    private Animator Animator;

    AudioManager audioManager;

	private void Awake(){
		audioManager = GameObject.FindGameObjectWithTag("AudioMan").GetComponent<AudioManager>();
	}


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
                audioManager.PlaySFX(audioManager.ElectricZap);
                Animator.SetBool("Attacking", true);
            }
        }
    }

    void AttackDone()
    {
        timer = cooldown;
        Animator.SetBool("Attacking", false);
    }

    void Attack()
    {
        if (playerAnimator.lookingRight)
        {
            mageAttack.GetComponent<PlayerProjectile>().direction = new Vector2(1, 0);
            Instantiate(mageAttack, markerRight.position, Quaternion.identity);
        }
        else
        {
            mageAttack.GetComponent<PlayerProjectile>().direction = new Vector2(-1, 0);
            Instantiate(mageAttack, markerLeft.position, Quaternion.identity);
        }
    }
}
