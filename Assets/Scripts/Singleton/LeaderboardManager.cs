using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    delegate int ScoreDelegate(PlayerController player);   
    
    private void Start() 
    {
        /*Al lancio del gioco cerco il player e lo registro all'evento tramite +=*/
        //FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
    }

    void Update() 
    {
        
    }

    public void OnPlayerDeath()
    {
        Debug.Log("Check Evento Morte Player");
        /*Alla morte del player e lo rimuovo dall'evento tramite -=*/
        //FindObjectOfType<PlayerController>().deathEvent -= OnPlayerDeath;
    }
    /*
    private void OnGameOver(PlayerController[] allPlayers)
    {
        string playerNameMostAssist = GetMVP(allPlayers, player => player.assists);
        string playerNameMostFrags = GetMVP(allPlayers, player => player.frags);
    }

    private string GetMVP(PlayerController[] allPlayers, ScoreDelegate scoreCalculator)
    {
        string name = "";
        int bestScore = 0;
        foreach(PlayerController player in allPlayers)
        {
            int _score = scoreCalculator(player);
            if(bestScore > _score)
            {
                bestScore =  _score;
                name = player.name;
            }            
        }
        return name;
    }*/
}
