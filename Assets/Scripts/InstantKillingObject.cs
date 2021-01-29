using UnityEngine;

public class InstantKillingObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) /*AT LEAST ONE OBJECT HAS TO HAVE A RIGIDBODY COMPONENT TO MAKE THIS FUNCTION WORK*/
    {
        if(other != null)/*Just to be super sure :P */
        {            
            GameObject collidedObject = other.gameObject;
            if(collidedObject.GetComponent<HealthController>() != null)
            {                
              //  collidedObject.onLifeGained += OneUp;
                Debug.Log("DIE SCUM!");
                KillTarget(collidedObject.GetComponent<HealthController>());
                Destroy(this.gameObject);                
            }
            if(collidedObject.GetComponent<PlayerController>() != null)
            {                
                Debug.Log("I just hit an instant killing object!");
                Destroy(this.gameObject);
            }
        }    
    }

    public void KillTarget(HealthController _healthController)
    {
        _healthController.lives = 0;
    }
}
