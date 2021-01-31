using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerController playerController;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerController.gameObject)
        {
            return;
        }

        playerController.SetGroundedState(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundedState(true);
    }
}

