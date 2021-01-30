using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using Photon.Pun;

[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public int frags = 0;
    public int assists = 0;
    public delegate void OnPlayerDeathDelegate();
    public event OnPlayerDeathDelegate deathEvent;
    private HealthController healthController;
    public Animator animator;
    private bool grounded = true;
    private Rigidbody playerRigidbody;
    private Vector3 direction;
    public float rotateSpeed = 1;
    public float movementSpeed = 1;
    public float jumpSpeed = 1;
    private float _jumpSpeed = 0;
    //private Dictionary<string, int> directions = new Dictionary<string, int>() { { "N", 0 }, };

    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    private void Start() 
    {
        if (!PV.IsMine)
        {
            Object.Destroy(GetComponentInChildren<Camera>().gameObject);
            return;
        }

        healthController = GetComponent<HealthController>();
        if(!healthController)
        {
            healthController = gameObject.AddComponent<HealthController>();
        }
        playerRigidbody = GetComponent<Rigidbody>();
        //animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() 
    {
        if (!PV.IsMine)
            return;

        Debug.Log("transform.position.y  : " + transform.position.y);
        Debug.Log("grounded  : " + grounded);
        if (animator != null)
        {
            if (this.healthController.lives <= 0)
            {
                Death();
            }
            if (transform.position.y <= 1f)
            {
                grounded = true;
                if (animator != null)
                {
                    animator.SetBool("grounded", true);

                }
            }
            if (grounded == true)
            {
                if (Input.GetAxisRaw("Jump") > 0)
                {
                    _jumpSpeed = jumpSpeed;
                    grounded = false;
                    animator.SetBool("grounded", false);
                    animator.SetTrigger("Jump");

                    playerRigidbody.AddForce(new Vector3(0, _jumpSpeed, 0), ForceMode.Impulse);

                }
            }

            if (Input.GetAxisRaw("Attack") > 0 && animator.GetBool("Attacking") == false)
            {
                /*creo collider d'attacco*/
                animator.SetBool("Attacking", true);
            }
            else
            {
                animator.SetBool("Attacking", false);
            }

            if (Input.GetAxisRaw("Interact") > 0)
            {
                /*usa oggetto*/
                animator.SetTrigger("Interact");
            }
            Movement();
        }    

    }
    public void Death() 
    {
        animator.SetBool("Death", true);
        if (deathEvent != null)
        {
            deathEvent();
        }
    }
    
    public void Movement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");        

       
        direction = new Vector3(horizontalMove, 0.0f, verticalMove);

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetInteger("Move", 0);
        }

        playerRigidbody.MovePosition(transform.position + movementSpeed * Time.deltaTime * direction);
        animator.SetInteger("Move", 1);
    }

    private void LateUpdate()
    {
        // transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        //Debug.DrawRay();
    }
}
