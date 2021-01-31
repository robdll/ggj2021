using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent (typeof (HealthController))]
public class PlayerController : MonoBehaviour {
    
    [SerializeField] GameObject cameraHolder;

    [SerializeField] SingleShotEye activeAbility;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;


    public Animator animator;
    HealthController healthController;
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    
    //secondary attack variable
    public ParticleSystem rollFx;
    public float sprintSpeedUpTime = .01f;
    public float sprintDuration = .3f;
    public float sprintChargingSpeed = 3;

    Rigidbody rb;
    PhotonView PV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV && !PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

        
        healthController = GetComponent<HealthController>();
        if (!healthController)
        {
            healthController = gameObject.AddComponent<HealthController>();
        }
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        Look();
        Move();
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activeAbility.Use();
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * mouseSensitivity);
        //verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation += Mathf.Clamp(verticalLookRotation, -90f, 90f);
        // vertical axis for camera?
        //cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }

    void Move()
    {
        Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
    {
        animator.SetBool("grounded", grounded);
        if (Input.GetAxisRaw("Jump") > 0 &&  grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void Attack()
    {
        if (Input.GetAxisRaw("Attack") > 0 && animator.GetBool("Attacking") == false)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    void SecondaryAttack()
    {
        if (Input.GetAxisRaw("Interact") > 0)
        {
            //sprint();
            animator.SetTrigger("Interact");
        }
        //if (timeStamp <= Time.time)
        //{
        //  timeStamp = Time.time + rollCooldown;
        //}
    }

    void FixedUpdate()
    {
        if (PV && !PV.IsMine)
            return;

        //animator.SetBool("grounded", grounded);
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (animator != null)
        {
            if (this.healthController.lives <= 0)
            {
                Death();
            }
            Jump();
            Attack();
            SecondaryAttack();
        }
     }

     public void SetGroundedState(bool _grounded)
     {
         grounded = _grounded;
     }

     private void sprint()
     {
         //  Invoke("stopRollFx", .5f);
         Sequence sprintSequence = DOTween.Sequence();
         sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, sprintChargingSpeed, .3f).SetEase(Ease.OutQuad));
         sprintSequence.AppendCallback(() => { rollFx.Play(); });
         sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, sprintSpeed, sprintSpeedUpTime));
         sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, walkSpeed , sprintDuration).SetEase(Ease.OutQuad));
         sprintSequence.AppendCallback(() => { rollFx.Stop(); });
     }

     private void stopSprint()
     {
         //movementSpeed = defaultSpeed;
     }

     private void stopRollFx()
     {
         rollFx.Stop();
     }

     /*
     public int score = 0;
     public int frags = 0;
     public int assists = 0;
     public delegate void OnPlayerDeathDelegate ();
     public event OnPlayerDeathDelegate deathEvent;
     private HealthController healthController;
     public Animator animator;
     private bool grounded = true;
     public float rollCooldown = 2;
     public float timeStamp;

     public float jumpSpeed = 1;
     private float _jumpSpeed = 0;
     [SerializeField]
     private float rayLength = 1;
     //private Dictionary<string, int> directions = new Dictionary<string, int>() { { "N", 0 }, };



     void FixedUpdate () {


     }
     public void Death () {
         animator.SetBool ("Death", true);
         if (deathEvent != null) {
             deathEvent ();
         }
     }


    */
    public void Death()
    {

    }
}