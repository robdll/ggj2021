using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;


    public void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                return;
            }
            playerController.SetGroundedState(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                return;
            }
            playerController.SetGroundedState(false);

        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject == playerController.gameObject)
            {
                return;
            }

            playerController.SetGroundedState(true);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
                return;
            }
            playerController.SetGroundedState(true);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
                return;
            }
            playerController.SetGroundedState(false);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject == playerController.gameObject)
            {
                return;
            }
            playerController.SetGroundedState(true);

        }
    }
}

