using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int lives = 3;
    public int hp = 3;

    public delegate void OnDeath();
    public delegate void OnLifeGained(HealthController _hc);
    public OnDeath onDeath;
    public OnLifeGained onLifeGained;

    private void Start()
    {
        onDeath += Death;
    }

    void Update() 
    {
        if(this.lives <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        Debug.Log("YOU DIED!!!");

    }

    public void OneUp() 
    {
        this.lives++;
    }
}
