using UnityEngine;

public class Pause : MonoBehaviour
{   
    private CreditsManager creditsManager;
    public void Awake()
    {
        Time.timeScale = 0;
        creditsManager = FindObjectOfType<CreditsManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            HidePauseMenu();
        }
    }
    public void HidePauseMenu()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        ScenesManager.Instance.SceneChange(0);
    }

    public void ShowCreditsPanel()
    {
        creditsManager.ShowCredits();
    }
}
