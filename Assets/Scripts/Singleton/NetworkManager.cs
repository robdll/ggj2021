using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkManager : MonoBehaviour
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
    }

    public void CreateController()
    {
        int randomSpawnerIndex = Random.Range(0, allSpawners.Length - 1);
        Transform spawningPoint = allSpawners[randomSpawnerIndex].transform;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawningPoint.position, Quaternion.identity);
        Debug.Log("init controller here");
        // firstTime = false;
    }

    public void SpawnBullet(Vector3 spawningPosition)
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), spawningPosition, Quaternion.identity);
    }

    public void SpawnRat(Transform t)
    {
        int randomSpawnerIndex = Random.Range(0, allSpawners.Length - 1);
        Transform spawningPoint = allSpawners[randomSpawnerIndex].transform;
        //t.position = spawningPoint.position + (Vector3.up * 0.3f);
        Debug.Log("instantiate rat");
        // firstTime = false;
    }
}
