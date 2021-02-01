using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Breakable : MonoBehaviour, IDamageable
{
    //public int lives = 3;
    public int hp = 3;
    public int maxHp = 3;
    PhotonView PV;
  //  public delegate void OnDeath();
  //   public delegate void OnLifeGained(HealthController _hc);
  //   public OnDeath onDeath;
  // public OnLifeGained onLifeGained;
  //  private PlayerController player;

    // public ParticleSystem explosion;
    /*
        private void Start()
        {
            onDeath += Death;
            player = GetComponent<PlayerController>();
        }
    */
    private void Update()
    {
       /* if (lives <= 0)
        {
            if (player != null)
            {

                GameOver();
            }
        }*/
        if (hp <= 0)
        {
            Death();
        }

    }
    public void Death()
    {
       // Instantiate(explosion, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(gameObject);
    }


    public void TakeDamage(float _damage)
    {

    }

    public void TakeDamage(int damage)
    {
         PV = GetComponent<PhotonView>(); 
        if (PV != null)
        {
            PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }        
        hp -= damage;
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        if (PV.IsMine)
        {
            return;
        }

        Debug.Log("took damage" + damage);
    }
}
