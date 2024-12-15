using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject currentScene;
    void Start()
    {
        currentScene = scene1;
    }
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (currentScene == scene1)
            {
                scene1.SetActive(false);
                scene2.SetActive(true);
                currentScene = scene2;
            }
            else if (currentScene == scene2)
            {
                scene2.SetActive(false);
                scene3.SetActive(true);
                currentScene = scene3;
            }
            else if (currentScene == scene3)
            {
                GameManager.instance.currentScene = "Screen1";
                SceneManager.LoadScene("Screen1");
            }
        }
    }
}
