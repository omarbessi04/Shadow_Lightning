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
    private Color originalColor;
    private TextMeshProUGUI textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
        originalColor = textComponent.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(OnHover());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(OnExit());
    }

    private IEnumerator OnHover()
    {
        // button.transform.localScale += new Vector3(3, 1, 0);
        float tick = 0f;
        while (textComponent.color != Color.red)
        {
            Debug.Log("Changing to RED");
            tick += Time.deltaTime * animationSpeed;
            textComponent.color = Color.Lerp(originalColor, Color.red, tick);
            yield return null;
        }
    }

    private IEnumerator OnExit()
    {
        // button.transform.localScale -= new Vector3(3, 1, 0);
        float tick = 0f;
        while (textComponent.color != originalColor)
        {
            Debug.Log("Changing to DEFAULT");
            tick += Time.deltaTime * animationSpeed;
            textComponent.color = Color.Lerp(changeColor, originalColor, tick);
            yield return null;
        }
    }
}
