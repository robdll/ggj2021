using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public Spawner[] allSpawners;

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

    public void CreateController()
    {
        int randomSpawnerIndex = Random.Range(0, allSpawners.Length-1);
        Transform spawningPoint = allSpawners[randomSpawnerIndex].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawningPoint.position, Quaternion.identity);
        Debug.Log("init controller here");
       // firstTime = false;
    }

    public void Respawn(Transform t)
    {
        int randomSpawnerIndex = Random.Range(0, allSpawners.Length - 1);
        Transform spawningPoint = allSpawners[randomSpawnerIndex].transform;
        t.position =allSpawners[randomSpawnerIndex].transform.position +(Vector3.up *0.3f);
        Debug.Log("init controller here");
        // firstTime = false;
    }
}
