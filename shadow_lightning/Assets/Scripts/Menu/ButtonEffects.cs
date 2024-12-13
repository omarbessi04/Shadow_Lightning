using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;
    public float animationSpeed;
    public Color changeColor;
    public Vector3 changeSize;
    private Color originalColor;
    private Vector3 originalSize;
    private TextMeshProUGUI textComponent;
    private bool isDoingHover;
    private bool isDoingUnHover;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
        originalColor = textComponent.color;
        originalSize = button.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // textComponent.color = Color.red;
        if (!isDoingHover)
            StartCoroutine(OnHover());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // textComponent.color = Color.black;
        if (!isDoingUnHover)
            StartCoroutine(OnExit());
    }

    private IEnumerator OnHover()
    {
        // button.transform.localScale += new Vector3(3, 1, 0);
        float tick = 0f;
        isDoingHover = true;
        while (isDoingUnHover)
        {
            yield return null;
        }
        while (textComponent.color != changeColor)
        {
            tick += Time.deltaTime * animationSpeed;
            textComponent.color = Color.Lerp(originalColor, changeColor, tick);
            button.transform.localScale = Vector3.Lerp(originalSize, changeSize, tick);
            yield return null;
        }
        isDoingHover = false;
    }

    private IEnumerator OnExit()
    {
        float tick = 0f;
        isDoingUnHover = true;
        while (isDoingHover)
        {
            yield return null;
        }
        while (textComponent.color != originalColor)
        {
            tick += Time.deltaTime * animationSpeed;
            textComponent.color = Color.Lerp(changeColor, originalColor, tick);
            button.transform.localScale = Vector3.Lerp(changeSize, originalSize, tick);
            yield return null;
        }
        isDoingUnHover = false;
    }
}
