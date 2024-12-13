using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class swordGuyRespawner : MonoBehaviour
{
    public GameObject currentSword;

    public GameObject sword;
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = currentSword.transform.position;
    }

    void Update()
    {
        if (currentSword == null)
        {
            currentSword = Instantiate(sword,spawnPoint, quaternion.identity);
        }
    }
}
