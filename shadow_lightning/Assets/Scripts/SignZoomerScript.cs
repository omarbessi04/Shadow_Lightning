using System.Collections;
using Unity.VisualScripting;
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
    public GameObject TransformingSign;
    public float initialY;
    public float targetHeight;

    private void Awake() {
        TransformingSign.GetComponent<Transform>().localScale = minScale;
        initialY = TransformingSign.GetComponent<Transform>().position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.canZoom = false;
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerEnemy")){
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ScaleToTarget(maxScale, true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.canZoom = true;
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerEnemy")){
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ScaleToTarget(minScale, false));
        }
    }

    private IEnumerator ScaleToTarget(Vector3 targetScale, bool ScalingUp)
    {
        WorkingOnIt = true;

        while (Vector3.Distance(TransformingSign.transform.localScale, targetScale) > 0.01f)
        {
            //Move Up
            if (ScalingUp){
                float newY = Mathf.MoveTowards(TransformingSign.transform.position.y, initialY + targetHeight, 4*Time.deltaTime);
                TransformingSign.transform.position = new Vector3(TransformingSign.transform.position.x, newY, TransformingSign.transform.position.z);
            }else{
                float newY = Mathf.MoveTowards(TransformingSign.transform.position.y, initialY, 6*Time.deltaTime);
                TransformingSign.transform.position = new Vector3(TransformingSign.transform.position.x, newY, TransformingSign.transform.position.z);
            }

            //Scale up
            TransformingSign.transform.localScale = Vector3.SmoothDamp(TransformingSign.transform.localScale, targetScale, ref velocity, smoothTime);
            yield return null;
        }

        // Make sure things are in the right place afterwards
        TransformingSign.transform.localScale = targetScale;
        if (ScalingUp){
            TransformingSign.GetComponent<Transform>().position = new Vector3(TransformingSign.transform.position.x, initialY + targetHeight, TransformingSign.transform.position.z);
        }else{
            TransformingSign.GetComponent<Transform>().position = new Vector3(TransformingSign.transform.position.x, initialY, TransformingSign.transform.position.z);
        }
        WorkingOnIt = false;
    }
}
