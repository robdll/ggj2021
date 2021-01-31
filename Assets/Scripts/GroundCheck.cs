using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter 1");
        if (other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                Debug.Log("OnTriggerEnter 2");
                return;
            }
            Debug.Log("OnTriggerEnter 3");
            playerController.SetGroundedState(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit1");
        if (other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                Debug.Log("OnTriggerExit2");
                return;
            }
            Debug.Log("OnTriggerExit 3");
            playerController.SetGroundedState(false);
        }
    }

    public void OnTriggerStay(Collider other) { 
                Debug.Log("OnTriggerExit1");
        if (other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                Debug.Log("OnTriggerExit2");

                return;
            }
            Debug.Log("OnTriggerStay 3");
            playerController.SetGroundedState(true);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
            Debug.Log("OnCollisionEnter1");
        if(collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
            Debug.Log("OnCollisionEnter2");
                return;
            }
            Debug.Log("OnCollisionEnter3");
            playerController.SetGroundedState(true);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
            Debug.Log("OnCollisionExit1");
        if (collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
            Debug.Log("OnCollisionExit2");
                return;
            }
            Debug.Log("OnCollisionExit3");
            playerController.SetGroundedState(false);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
            Debug.Log("OnCollisionStay1");
        if (collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
            Debug.Log("OnCollisionStay2");
                return;
            }
            Debug.Log("OnCollisionStay3");
            playerController.SetGroundedState(true);

        }
    }
}

