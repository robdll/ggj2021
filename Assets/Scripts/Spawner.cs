using Photon.Pun;
using UnityEngine;

public abstract class Spawner : MonoBehaviourPunCallbacks
{
    public abstract void Spawn(GameObject objectToSpawn);
    public abstract void Respawn(GameObject objectToRepawn);
    public abstract void SpawnMultiple(GameObject[] objectsToSpawn);
    public abstract void RespawnMultiple(GameObject[] objectsToRepawn);
}
