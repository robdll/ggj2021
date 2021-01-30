using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 cameraOffset;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
        playerTransform = GetComponentInParent<PlayerController>().transform;
        if (playerTransform != null)
        {
            cameraOffset = cam.transform.position - playerTransform.position;
        }

    }
    // Update is called once per frame
    void Update()
    {
        //transform.rotation = 
        //cam.transform.position = playerTransform.position + cameraOffset;
    }
}