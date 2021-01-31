using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    public Transform playerTranform;
    [HideInInspector]
    public PlayerController player;

    void Start()
    {
        playerTranform = player.transform;
        if (playerTranform!=null)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(playerTranform.transform.forward * bulletSpeed, transform.position, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Debug.Log("HO COLPITO " + collision.gameObject.name);
        }
    }

}
