using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {

        // In Game Scene we build a Room manager prefab.
        // PhotonPrefabs must be in the resources older
        // unity excludes any file not referenced in the editor from the final build
        if(scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate( Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity );
        }
    }

}
