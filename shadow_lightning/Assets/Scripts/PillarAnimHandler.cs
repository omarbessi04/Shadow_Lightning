using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAnimHandler : MonoBehaviour
{
    public Animator my_animator;

    private void Update() {
        if (GameManager.instance.alive_enemy_count == 0){
            my_animator.SetBool("RoomCleared", true);
        }
    }
}
