using UnityEngine;
using Photon.Pun;
using System.IO;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    [HideInInspector]
    public PlayerController player;
    public int bulletDamage = 1;
    //public ParticleSystem collisionParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            //   Debug.Log("HO COLPITO " + collision.gameObject.name);

            if (collision.gameObject.GetComponent<HealthController>() != null)
            {
                collision.gameObject.GetComponent<HealthController>().TakeDamage(bulletDamage);
            }
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/ParticlesEffects", "EnergyExplosion"), transform.position, Quaternion.identity);

           //Instantiate(collisionParticles, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    
    }


}
