using UnityEngine;

public class GameOver : MonoBehaviour
{    public void ReturnToMainMenu()
    {
        ScenesManager.Instance.SceneChange(0);
    }
}
