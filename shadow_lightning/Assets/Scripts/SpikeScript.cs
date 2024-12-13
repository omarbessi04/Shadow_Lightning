using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Vector3 wantedPosition;
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            GameManager.instance.heartSystem.TakeDamage(0.5f);
            other.GetComponent<PossessController>().unpossessCooldown = 0;
            other.GetComponent<Transform>().position = wantedPosition;

        }else if (other.CompareTag("PlayerEnemy")){
            GameManager.instance.heartSystem.TakeDamage(0.5f);
            other.GetComponent<Transform>().position = wantedPosition;

        }else{
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().takeDamage(500);
            }
        }
    }
}
