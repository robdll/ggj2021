using UnityEngine;
using UnityEngine.UIElements;

public class CreditsManager : MonoBehaviour
{  
   /* public GameObject LoadingImage;*/ /*Nice image moving during loading screens*/
    public GameObject creditsPanel; /*Panel that will be instantiated when a scene change is called*/
    [HideInInspector]
    public Image creditsImage; /*Background image of the loading screen*/

    public void ShowCredits()
    {
        if(creditsPanel != null)
        {
            Instantiate(creditsPanel);
        }
    }

    public void HideCredits()
    {
        Credits[] credits = FindObjectsOfType<Credits>();
        for(int i = 0; i < credits.Length; i++)
        {
            if(credits[i] != null)
            {
                Destroy(credits[i].gameObject);
            }            
        }
    }
}
