using UnityEngine;

public class PillarAnimHandler : MonoBehaviour
{
    public Animator my_animator;
    public float moveSpeed = 1f;
    public float targetHeight = 5f;
    public bool isMovingUp = false;
    private float initialY;
    public bool hasMovedUp = false;

    public Camera mainCamera;
    public Vector3 cameraOffset = new Vector3(0, 5, -10);
    public float cameraMoveSpeed = 2f;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        if (GameManager.instance.alive_enemy_count == 0 && !hasMovedUp)
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

        // Move the camera to focus on this object
        GameManager.instance.ShowingPillar = true;
        StartCoroutine(FocusCamera());
    }

    private System.Collections.IEnumerator FocusCamera()
    {
        Vector3 targetPosition = transform.position + cameraOffset;

        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);

            // Rotate the camera to look at the pillar
            mainCamera.transform.LookAt(transform.position);

            yield return null;
        }

        // Ensure the final rotation looks directly at the pillar
        mainCamera.transform.LookAt(transform.position);
    }
}
