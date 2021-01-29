using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    private void Start() 
    {
        FindObjectOfType<PlayerController>().deathEvent += OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        FindObjectOfType<PlayerController>().deathEvent -= OnPlayerDeath;
    }
}
