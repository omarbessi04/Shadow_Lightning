using System;
using System.Collections;
using UnityEngine;

public class CameraEffectScript : MonoBehaviour
{
    private float zoom;
    private float zoomMultiplier = 4f;
    public float minZoom = 4f;
    public float maxZoom = 8f;
    private float velocity = 0.1f;
    private float smoothTime = 0.25f;
    public bool ZIWorkingOnIt = false;
    public bool ZOWorkingOnIt = false;
    private Coroutine currentCoroutine = null;

    [SerializeField] private Camera cam;

    void Start()
    {
        zoom = cam.orthographicSize;
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
        ZIWorkingOnIt = true;

        while (Math.Abs(cam.orthographicSize - minZoom) > 0.01f)
        {
            zoom -= 0.1f * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
            yield return null; // Use a frame delay for more responsive checks
        }

        cam.orthographicSize = minZoom;
        ZIWorkingOnIt = false;
    }

    private IEnumerator ZoomOut()
    {
        ZOWorkingOnIt = true;

        while (Math.Abs(cam.orthographicSize - maxZoom) > 0.01f)
        {
            zoom += 0.1f * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
            yield return null; // Use a frame delay for more responsive checks
        }

        cam.orthographicSize = maxZoom;
        ZOWorkingOnIt = false;
    }
}
