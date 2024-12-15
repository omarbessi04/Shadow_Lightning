using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Vector3 wantedPosition;
    public bool respawnEnemies = false;
    public bool scene5 = false;
    public bool makeEnemyDie = false;
    private void OnTriggerEnter2D(Collider2D other) {

        
        if (other.CompareTag("Player")) {
            GameManager.instance.heartSystem.TakeDamage(0.5f);
            GameManager.instance.unpossessTimer = 0;
            other.GetComponent<Transform>().position = wantedPosition;


        }else if (other.CompareTag("PlayerEnemy")){
            GameManager.instance.heartSystem.TakeDamage(0.5f);
            other.GetComponent<Transform>().position = wantedPosition;
            GameManager.instance.unpossessTimer = 0;

        }else{
            if (other.tag == "Enemy")
            {
                if (respawnEnemies && !scene5)
                {
                    other.transform.position = wantedPosition;
                }
                else if (respawnEnemies && scene5 && !makeEnemyDie)
                {
                    if (GameManager.instance.alive_enemy_count == 1)
                    {
                        other.transform.position = wantedPosition;
                    }
                    else
                    {
                        other.GetComponent<EnemyHealth>().takeDamage(500);
                    }
                }
                else if (makeEnemyDie && GameManager.instance.alive_enemy_count == 1)
                {
                    other.GetComponent<EnemyMovement>().goingRight = true;
                    if (!other.GetComponent<EnemyMovement>().flippedRight)
                    {
                        other.GetComponent<EnemyMovement>().flipX(true);
                    }
                    other.GetComponent<EnemyMovement>().shouldmoveWaypoints = true;
                    makeEnemyDie = false;
                    respawnEnemies = false;
                    other.transform.position = wantedPosition;
                }
                else
                {
                    other.GetComponent<EnemyHealth>().takeDamage(500);
                }
            }
        }
    }
}
