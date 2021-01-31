using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject roomManagerPrefab;

    private void Start()
    {
        Instantiate(roomManagerPrefab);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            canvasObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
