using System.Transactions;
using UnityEngine;

public class MiniBossPillarAnimHandeler : MonoBehaviour
{
    public Animator my_animator;
    public float moveSpeed = 1f;
    public float targetHeight = 5f;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private float initialY;
    private float initialX;

    private void Start()
    {
        initialY = transform.position.y;
        initialX = transform.position.x;
    }

    private void Update()
    {
        Debug.Log($"Up: {isMovingUp}, Down: {isMovingUp}");
        
        if (isMovingUp)
        {
            float newY = Mathf.MoveTowards(transform.position.y, initialY + targetHeight, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (Mathf.Approximately(transform.position.y, initialY + targetHeight))
            {
                isMovingUp = false;
            }

        } else if(isMovingDown){

            float newY = Mathf.MoveTowards(transform.position.y, initialY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (Mathf.Approximately(transform.position.y, initialY))
            {
                isMovingDown = false;
            }
        }
    }

    public void MoveUp(){
        my_animator.SetBool("RoomCleared", true);
        isMovingUp = true;
    }

    public void MoveDown(){
        my_animator.SetBool("RoomCleared", false);
        isMovingUp = false;
        isMovingDown = true;
    }
}
