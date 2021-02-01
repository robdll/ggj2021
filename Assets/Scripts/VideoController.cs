using Photon.Pun;
using UnityEngine;

public class VideoController : MonoBehaviour
{
   public GameObject canvasObject;
    

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            /*GameObject roomManager = Instantiate(new GameObject("Room Manager"));
            if(roomManager.GetComponent<RoomManager>() != null)
            {
                gameObject.AddComponent<RoomManager>();
            }
            if (roomManager.GetComponent<PhotonView>() != null)
            {
                gameObject.AddComponent<PhotonView>();
            }*/
            canvasObject.SetActive(true);            
            Destroy(gameObject);
        }
    }
}
