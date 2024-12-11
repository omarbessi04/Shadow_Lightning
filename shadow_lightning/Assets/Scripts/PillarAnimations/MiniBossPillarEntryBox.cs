using UnityEngine;

public class MiniBossPillarHandeler : MonoBehaviour
{
    [SerializeField] GameObject pillar;
    private void OnTriggerEnter2D(Collider2D other) {
        pillar.GetComponent<MiniBossPillarAnimHandeler>().MoveUp();
    }

    private void OnTriggerExit2D(Collider2D other) {
        pillar.GetComponent<MiniBossPillarAnimHandeler>().MoveDown();
    }
}
