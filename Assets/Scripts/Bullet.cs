using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    [HideInInspector]
    public Transform playerTranform;
    [HideInInspector]
    public PlayerController player;
    public int bulletDamage = 1;
    public ParticleSystem collisionParticles;

    void Start()
    {
        playerTranform = player.transform;
        if (playerTranform!=null)
        {
            /*DA DECOMMENTARE*/
        //    GetComponent<Rigidbody>().AddForceAtPosition(playerTranform.transform.forward * bulletSpeed, transform.position, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
         //   Debug.Log("HO COLPITO " + collision.gameObject.name);
            if(collision.gameObject.GetComponent<HealthController>() != null)
            {
                collision.gameObject.GetComponent<HealthController>().(bulletDamage);
            }
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    
    }


}
