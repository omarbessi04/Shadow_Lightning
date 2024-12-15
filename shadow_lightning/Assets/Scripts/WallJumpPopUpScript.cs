using System.Collections;
using UnityEngine;

public class WallJumpPopUpScript : MonoBehaviour
{
    private bool canDismiss = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Unlock()
    {
        GameManager.instance.PlayerHasWallJump = true;
        
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject child = transform.parent.GetChild(i).gameObject;
            if (child != gameObject)
            {
                child.SetActive(false);
            }
        }
        
        gameObject.SetActive(true);
        StartCoroutine(ShowAndEnableDismiss());
    }

    private void Update()
    {
        if (canDismiss && Input.anyKeyDown)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                GameObject child = transform.parent.GetChild(i).gameObject;
                if (child != gameObject)
                {
                    child.SetActive(true);
                }
            }
            
            GameObject player = GameObject.FindGameObjectWithTag("PlayerEnemy") ??
                                GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
            }
            
            Destroy(gameObject);
        }
    }

    private IEnumerator ShowAndEnableDismiss()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerEnemy") ??
                            GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        
        yield return new WaitForSeconds(2f);
        
        canDismiss = true;
    }
}
