using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    [SerializeField] float DoomTimer;
    void Start()
    {
        Destroy(gameObject, DoomTimer);
    }
}
