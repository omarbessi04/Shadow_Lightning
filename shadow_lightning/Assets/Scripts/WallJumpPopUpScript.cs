using System.Collections;
using System.Data;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class WallJumpPopUpScript : MonoBehaviour
{
    bool reading = false;
    private void Start() {
        gameObject.SetActive(false);
    }

    public void Unlock(){
        GameManager.instance.PlayerHasWallJump = true;
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject child = transform.parent.GetChild(i).GameObject();
            if (child != gameObject)
            {
                child.SetActive(false);
            }
        }
        gameObject.SetActive(true);
        StartCoroutine(Deletion());
    }

    private void Update() {

        if (reading){
            
            if (Input.anyKeyDown){
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    GameObject child = transform.parent.GetChild(i).GameObject();
                    if (child != gameObject)
                    {
                        child.SetActive(true);
                    }
                }
                GameObject obj = GameObject.FindGameObjectWithTag("PlayerEnemy");
                if (!obj) GameObject.FindGameObjectWithTag("Player");
                obj.GetComponent<PlayerMovement>().enabled = true;
                reading = false;
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Deletion(){

        GameObject obj = GameObject.FindGameObjectWithTag("PlayerEnemy");
        if (!obj) GameObject.FindGameObjectWithTag("Player");
        obj.GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(2f);

        reading = true;
    }
}
