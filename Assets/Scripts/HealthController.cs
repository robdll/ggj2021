using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class HealthController : MonoBehaviour, IDamageable
{
    public int maxLives = 3;
    public int lives = 3;
    public int hp = 3;
    public int maxHp = 3;
    public delegate void OnDeath();
    public delegate void OnLifeGained(HealthController _hc);
    public OnDeath onDeath;
    public OnLifeGained onLifeGained;
    public TMP_Text livesTxt;
    public TMP_Text hpTxt;
    public ParticleSystem explosion;
    [HideInInspector]
    public PlayerController player;
    

    private void Start()
    {
        onDeath += Death;
        player = GetComponent<PlayerController>();
        if(player != null)
        {
            livesTxt.text = "Lives: " + lives + " / " + maxLives;
            hpTxt.text = "Hp: " + hp + " / " + maxHp;
        }
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
            hp = maxHp;
            player.Die();            
        }
        else
        {
           // PhotonNetwork.Instantiate(explosion, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
       Debug.Log("YOU DIED!!!");        
    }

    public void OneUp() 
    {
        this.lives++;
        if (player != null)
        {
            livesTxt.text = "Lives: " + lives + " / " + maxLives;
            hpTxt.text = "Hp: " + hp + " / " + maxHp;
        }
    }

    public void LoseLife()
    {
        --lives;
        if (player != null)
        {
            livesTxt.text = "Lives: " + lives + " / " + maxLives;
            hpTxt.text = "Hp: " + hp + " / " + maxHp;
        }
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
            if (PV!=null)
            {
                PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
            }
            livesTxt.text = "Lives: " + lives + " / " + maxLives;
            hpTxt.text = "Hp: " + hp + " / " + maxHp;            
        } 

        hp -= damage;
    }
}
