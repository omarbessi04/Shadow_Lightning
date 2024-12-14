using System.Collections;
using UnityEngine;

public class SignScalerScript : MonoBehaviour
{
    [Header("General")]
    public bool WorkingOnIt = false;
    private Coroutine currentCoroutine = null;

    [Header("--- Items for Scaling ---")]
    public Vector3 minScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxScale;
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private void Awake() {
        GetComponent<Transform>().localScale = minScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleToTarget(maxScale));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleToTarget(minScale));
    }

    private IEnumerator ScaleToTarget(Vector3 targetScale)
    {
        WorkingOnIt = true;

        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, smoothTime);
            yield return null; // Wait for the next frame
        }

        transform.localScale = targetScale;
        WorkingOnIt = false;
    }
}
