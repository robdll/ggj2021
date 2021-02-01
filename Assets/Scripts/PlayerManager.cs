using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public Spawner[] allSpawners;
    //private bool firstTime = true;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        allSpawners = FindObjectsOfType<Spawner>();
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        int randomSpawnerIndex = Random.Range(0, allSpawners.Length-1);
        Transform spawningPoint = allSpawners[randomSpawnerIndex].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawningPoint.position, Quaternion.identity);
        Debug.Log("init controller here");
       // firstTime = false;
    }
}
