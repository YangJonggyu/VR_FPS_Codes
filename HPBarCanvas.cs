using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarCanvas : MonoBehaviour
{
    public GameObject playerCamera;
    public Canvas canvas;

    void Update()
    {
        var rotation = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
        transform.SetPositionAndRotation(playerCamera.transform.position,Quaternion.Euler(rotation));
        canvas.transform.forward = Vector3.Normalize(canvas.transform.position - playerCamera.transform.position);
    }
}
