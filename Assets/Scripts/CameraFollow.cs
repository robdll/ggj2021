using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 cameraOffset;
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        if(playerTransform != null)
        {
            cameraOffset = Camera.main.transform.position - playerTransform.position;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = playerTransform.position + cameraOffset;
    }
}
