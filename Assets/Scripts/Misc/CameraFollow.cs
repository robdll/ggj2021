using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraOffset;
    private Camera cam;
    private Quaternion cameraRot;
    private Transform playerTransform;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cameraRot = cam.transform.rotation;
        playerTransform = GetComponentInParent<PlayerController>().transform;
        if (playerTransform != null)
        {
            cameraOffset = cam.transform.position - playerTransform.position;
        }
    }

    void Update()
    {
        cam.transform.rotation = cameraRot;
        cam.transform.position = playerTransform.position + cameraOffset;
    }
}