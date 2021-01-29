using UnityEngine;
using UnityEngine.UIElements;

public class GameOverManager : MonoBehaviour
{  
   /* public GameObject LoadingImage;*/ /*Nice image moving during loading screens*/
    public GameObject gameOverPanel; /*Panel that will be instantiated when a scene change is called*/
    [HideInInspector]
    public Image gameOverImage; /*Background image of the loading screen*/

    public void ShowGameOver()
    {
        if(gameOverPanel != null)
        {
            Instantiate(gameOverPanel);
        }
    }
}
