using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}