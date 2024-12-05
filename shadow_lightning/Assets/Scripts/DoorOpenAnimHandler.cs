using UnityEngine;

public class DoorOpenAnimHandler : MonoBehaviour
{

    GameObject player;
	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

    public void HidePlayer(){
        player.SetActive(false);
    }

    public void ShowPlayer(){
        player.SetActive(true);
    }
    public void DestroyDoorAfterAnim(){
        Debug.Log("Destroyed " + gameObject);
        Destroy(gameObject);
    }
}