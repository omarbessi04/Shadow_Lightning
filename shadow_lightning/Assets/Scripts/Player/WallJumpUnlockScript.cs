using UnityEngine;

public class WallJumpUnlockScript : MonoBehaviour
{
    PlayerMovement player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerEnemy")){
            GameManager.instance.PlayerHasWallJump = true;
        }
    }
}
