using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMageAttack : MonoBehaviour
{
    public GameObject mageAttack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(mageAttack, transform.position, Quaternion.identity);
        }
    }
}
