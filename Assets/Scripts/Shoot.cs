using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;

public class Shoot : Eye
{
    public GameObject bulletPrefab;
    public Transform gunHole;
    private Camera playerCam;
    public GameObject crosshair;
    private NetworkManager NM;
    

    public override void Use()
    {
        ShootBullet(); 
    }
    private void Start()
    {
        NM = GetComponentInParent<NetworkManager>();
        playerCam = GetComponentInParent<Camera>();
    }

  /* [PunRPC]
    void RPC_ShootBullet(int damage)
    {
        PhotonView PV = GetComponentInParent<PlayerController>().PV;
        if (!PV.IsMine)
        {
            return;
        }
        GameObject shotBullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), gunHole.position, Quaternion.identity);
        Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(SetDirection().normalized * bulletPrefab.GetComponent<Bullet>().bulletSpeed, ForceMode.Impulse);
        PlayerController pc = GetComponentInParent<PlayerController>();
        Bullet bullet = shotBullet.GetComponent<Bullet>();
        bullet.player = pc;
        Destroy(shotBullet, 5f);
        Debug.Log("Bullet shot");
    }*/

    public void ShootBullet()
    {
        PhotonView PV = GetComponentInParent<PhotonView>();
        if (PV != null)
        {
            PV.RPC("RPC_Shoot", RpcTarget.All);
        }
        GameObject shotBullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), gunHole.position, Quaternion.identity);
        Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(SetDirection().normalized  * bulletPrefab.GetComponent<Bullet>().bulletSpeed, ForceMode.Impulse);
        PlayerController pc = GetComponentInParent<PlayerController>();
        Bullet bullet = shotBullet.GetComponent<Bullet>();
        
        bullet.player = pc;

    }

    public Vector3 SetDirection()
    {
        Vector3 direction = CheckDirection() - gunHole.position;
        return direction;
    }

    public Vector3 CheckDirection()
    {
        Vector3 destination = Vector3.zero;
        RaycastHit hit;
        Ray ray = playerCam.ScreenPointToRay(crosshair.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if(hitObject.GetComponentInParent<PhotonView>() != null)
            {
                PhotonView _identity = hitObject.GetComponentInParent<PhotonView>();
                if (_identity.IsMine)
                { 
                    RaycastHit _hit;
                    Ray _ray = new Ray(gunHole.position, transform.forward + -transform.up  * 2);
                    if(Physics.Raycast(_ray, out _hit))
                    {
                        destination = _hit.point;
                      //  Instantiate(particleSystem, destination, Quaternion.identity); //ONLY FOR TEST PURPOSES!!!
                      //  Debug.Log("Sto Colpendo" + hit.collider.gameObject.name);
                    }
                }
                else
                {
                    destination = hit.point;
                   // Instantiate(particleSystem, destination, Quaternion.identity); //ONLY FOR TEST PURPOSES!!!;
                }
            }
            else
            {
                destination = hit.point;
              //  Instantiate(particleSystem, destination, Quaternion.identity); //ONLY FOR TEST PURPOSES!!!
            }
        }
        return destination;
    }    
}
