using UnityEngine;

public class WallJumpUnlockScript : MonoBehaviour
{
    PlayerMovement player;
    MiniBossPillarAnimHandeler pillar;

    private void Awake() {
        pillar = GameObject.FindGameObjectWithTag("Pillar").GetComponent<MiniBossPillarAnimHandeler>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerEnemy")){
            GameManager.instance.PlayerHasWallJump = true;
            pillar.MoveUp();
        }
    }
}
