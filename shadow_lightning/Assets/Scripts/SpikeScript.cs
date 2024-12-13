using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Vector3 wantedPosition;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnemy")){
            GameManager.instance.heartSystem.TakeDamage(1);
        }else{
            other.GetComponent<EnemyHealth>().takeDamage(1);
        }

        other.GetComponent<Transform>().position = wantedPosition;
    }
}
