using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene5spikes : MonoBehaviour
{
    public SpikeScript SpikeScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && GameManager.instance.alive_enemy_count == 1)
        {
            SpikeScript.makeEnemyDie = true;
            
        }
    }
}
