using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
    [SerializeField]
    Transform cameraPos;

    void Update()
    {
        transform.position = cameraPos.position;
    }
}