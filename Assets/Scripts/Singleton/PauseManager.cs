using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private Pause pause;     
    
    private void Update()
    {  
        if(Input.GetKeyDown(KeyCode.P) && ScenesManager.Instance.GetActualScene() > 0)
        {
            pause = FindObjectOfType<Pause>();
            if(pause == null)
            {
                ShowPauseMenu();
            }            
        }       
    }
    public void ShowPauseMenu()
    {
        Instantiate(pauseMenuPanel);
    }
}
