using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindPillarChecker : MonoBehaviour
{
    PillarAnimHandler pillarAnimHandler;

    private void Awake() {
        pillarAnimHandler = GameObject.FindGameObjectWithTag("Pillar").GetComponent<PillarAnimHandler>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (pillarAnimHandler.hasMovedUp == false && !pillarAnimHandler.isMovingUp){
            if(other.CompareTag("Player") || other.CompareTag("PlayerEnemy")){
                pillarAnimHandler.MoveUp();
            }
        }
    }
}
