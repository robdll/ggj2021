using UnityEngine;
using Photon.Pun;

public class HealthController : MonoBehaviour, IDamageable
{
    public int lives = 3;
    public int hp = 3;
    public int maxHp = 3;
    public delegate void OnDeath();
    public delegate void OnLifeGained(HealthController _hc);
    public OnDeath onDeath;
    public OnLifeGained onLifeGained;
    private PlayerController player;

    public ParticleSystem explosion;

    private void Start()
    {
        onDeath += Death;
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(lives <= 0)
        {
            if(player!=null)
            {

                GameOver();
            }
        }
        if(hp <= 0)
        {
            Death();
        }
        
    }
    public void Death()
    {
        if(player != null)
        {
            lives--;
            hp = maxHp;
            player.Death();
        }
        else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       Debug.Log("YOU DIED!!!");        
    }

    public void OneUp() 
    {
        this.lives++;
    }

    public void GameOver()
    {
        if(player != null)
        {
            player.GameOver();
        }

    }
    public void TakeDamage(float _damage)
    {
       
    }

    public void TakeDamage(int damage)
    {
        PhotonView PV;
        if (player != null)
        {
            PV = player.photonView;
            if (PV != null)
            {
                PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
            }
        } 


        hp -= damage;
    }
}
