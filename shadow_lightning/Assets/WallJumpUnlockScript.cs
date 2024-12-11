using UnityEngine;

public class WallJumpUnlockScript : MonoBehaviour
{
    PlayerMovement player;
    MiniBossPillarAnimHandeler pillar;

    [SerializeField] GameObject jumpText;

    private void Awake() {
        jumpText.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pillar = GameObject.FindGameObjectWithTag("Pillar").GetComponent<MiniBossPillarAnimHandeler>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        player.wallJumpClimb = new Vector2(7.5f, 16);
        player.wallJumpOff = new Vector2(8.5f, 7);
        player.wallLeap = new Vector2(18, 17);

        pillar.MoveUp();
        jumpText.SetActive(true);
    }
}
