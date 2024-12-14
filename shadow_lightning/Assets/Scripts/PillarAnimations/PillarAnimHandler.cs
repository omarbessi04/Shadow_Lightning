using UnityEngine;

public class PillarAnimHandler : MonoBehaviour
{
    public Animator my_animator;
    public float moveSpeed = 1f;
    public float targetHeight = 5f;
    public bool isMovingUp = false;
    private float initialY;
    public bool hasMovedUp = false;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        if (GameManager.instance.alive_enemy_count == 0)
        {
            MoveUp();
        }

        if (isMovingUp)
        {
            float newY = Mathf.MoveTowards(transform.position.y, initialY + targetHeight, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (Mathf.Approximately(transform.position.y, initialY + targetHeight))
            {
                isMovingUp = false;
                hasMovedUp = true;
            }
        }
    }

    public void MoveUp()
    {
        my_animator.SetBool("RoomCleared", true);
        isMovingUp = true;
    }
}