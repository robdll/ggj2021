using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int lives = 3;
    public int hp = 3;
    public int maxHp = 3;
    public delegate void OnDeath();
    public delegate void OnLifeGained(HealthController _hc);
    public OnDeath onDeath;
    public OnLifeGained onLifeGained;
    private PlayerController player;
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
    public void TakeDamage(int _damage)
    {
        this.hp -= _damage;
    }
}
