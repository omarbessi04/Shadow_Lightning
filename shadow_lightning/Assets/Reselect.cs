using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reselect : MonoBehaviour
{
    public EventSystem eventSystem;
    private GameObject lastSelectedObject;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastSelectedObject);
        }
        else
        {
            lastSelectedObject = eventSystem.currentSelectedGameObject;
        }
    }
}
