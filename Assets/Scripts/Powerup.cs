using UnityEngine;

public class Powerup : MonoBehaviour
{   
    private void OnCollisionEnter(Collision other) /*AT LEAST ONE OBJECT HAS TO HAVE A RIGIDBODY COMPONENT TO MAKE THIS FUNCTION WORK*/
    {
        if(other != null)/*Just to be super sure :P */
        {            
            GameObject collidedObject = other.gameObject;
            if(collidedObject.GetComponent<HealthController>() != null)
            {                
              //  collidedObject.onLifeGained += OneUp;
                Debug.Log("OneUp!");
                Destroy(this.gameObject);                
            }
            if(collidedObject.GetComponent<PlayerController>() != null)
            {                
                Debug.Log("Powerup Gained!");
                Destroy(this.gameObject);
            }
        }    
    }

    public void OneUp(HealthController _healthController)
    {
        _healthController.lives++;
    }
}
