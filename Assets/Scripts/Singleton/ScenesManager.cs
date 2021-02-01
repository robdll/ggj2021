using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    private float loadingScreenMinDuration = 2; 
   /* public GameObject LoadingImage;*/ /*Nice image moving during loading screens*/
    public GameObject loadingScreenPanel; /*Panel that will be instantiated when a scene change is called*/
    [HideInInspector]
    public Image loadingScreenImage; /*Background image of the loading screen*/
    
    [HideInInspector]
    public Camera mainCamera;

    public static ScenesManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {       
        DontDestroyOnLoad(gameObject); /*This keeps alive the OBJECT on witch it's attached (in our case the GameManager), after a scene change. Singleton Pattern*/
    }


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        mainCamera = Camera.main;
    }

    public void SceneChange(int index)
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.enabled = false;
        }
        StartCoroutine(CoLoadSceneAsyncAfterSeconds(0, loadingScreenMinDuration, index));
    }


    public void SceneChangeFast(int index)
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = false;
        }
        SceneManager.LoadScene(index);
    }


    public void SceneChange(int index, float delay)
    {
        StartCoroutine(CoLoadSceneAsyncAfterSeconds(delay, loadingScreenMinDuration, index));
    }

    private IEnumerator CoLoadSceneAsyncAfterSeconds(float delay, float time, int index)
    {        
        /*yield return new WaitForSeconds(delay);*/

        Instantiate(loadingScreenPanel);
        /*LoadingScreenPanel.SetActive(true);*/

        yield return new WaitForSecondsRealtime(0.2f);
        
        Time.timeScale = 0; /*TBC*/

        yield return new WaitForSecondsRealtime(time);

        var asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.completed += (x) => Time.timeScale = 1;

        if(SceneManager.GetActiveScene().buildIndex != 0 && asyncOperation.isDone)
        {
           /* if(LoadingImage != null)
            {
                LoadingImage.SetActive(false);
            }          */      
           /* LoadingScreenPanel.SetActive(false);*/
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public int GetActualScene()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        return SceneManager.GetActiveScene().buildIndex;
    }
}
