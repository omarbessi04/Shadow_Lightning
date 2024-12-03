using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMageAttack : MonoBehaviour
{
    public GameObject mageAttack;
    public PlayerAnimator playerAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerAnimator.lookingRight)
            {
                mageAttack.GetComponent<PlayerProjectile>().direction = new Vector2(1, 0);
                Instantiate(mageAttack, transform.position, Quaternion.identity);
            }
            else
            {
                mageAttack.GetComponent<PlayerProjectile>().direction = new Vector2(-1, 0);
                Instantiate(mageAttack, transform.position, Quaternion.identity);
            }
        }
    }
}
