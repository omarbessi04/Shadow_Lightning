using System.Collections;
using UnityEngine;

public class CameraEffectScript : MonoBehaviour
{
    [Header("General")]
    public bool WorkingOnIt = false;
    private Coroutine currentCoroutine = null;
    [SerializeField] private Camera cam;

    [Header("--- Items for Zooming ---")]
    private float zoom;
    private float zoomMultiplier = 4f;
    public float minZoom = 4f;
    public float maxZoom = 8f;
    private float velocity = 0.1f;
    private float smoothTime = 0.25f;
    
    [Header("--- Items for Screenshake ---")]
    private Vector3 originalPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1.0f;

    void Start()
    {
        zoom = cam.orthographicSize;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // If the screen is shaking, reduce duration and shake the camera
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            // Reduce the duration based on time
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Ensure camera resets to its original position
            shakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void StartZoom()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ZoomIn());
    }

    public void StopZoom()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn()
    {
        WorkingOnIt = true;

        while (Mathf.Abs(cam.orthographicSize - minZoom) > 0.01f)
        {
            zoom -= 0.1f * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
            yield return null; // Use a frame delay for more responsive checks
        }

        cam.orthographicSize = minZoom;
        WorkingOnIt = false;
    }

    private IEnumerator ZoomOut()
    {
        WorkingOnIt = true;

        while (Mathf.Abs(cam.orthographicSize - maxZoom) > 0.01f)
        {
            zoom += 0.1f * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
            yield return null; // Use a frame delay for more responsive checks
        }

        cam.orthographicSize = maxZoom;
        WorkingOnIt = false;
    }
}
