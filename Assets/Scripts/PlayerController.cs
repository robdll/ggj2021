using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject cameraHolder;

    [SerializeField] Eye[] activeAbility;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [HideInInspector]
    public NetworkManager networkManager;
    PlayerManager PM;
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

    /*
    public float rollCooldown = 2;
    public float timeStamp;

    public float jumpSpeed = 1;
    private float _jumpSpeed = 0;
    public ParticleSystem rollFx;
    public ParticleSystem jumpFx;
    public int jumpParticlesBurst = 30;
    //private Dictionary<string, int> directions = new Dictionary<string, int>() { { "N", 0 }, };
    */

    Rigidbody rb;
    public PhotonView PV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        /*Comment the line below when testing on the game scene*/
        PM = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        networkManager = FindObjectOfType<NetworkManager>();
    }

    void Start()
    {

        if (PV && !PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        PM = FindObjectOfType<PlayerManager>();

        healthController = GetComponent<HealthController>();
        if (!healthController)
        {
            healthController = gameObject.AddComponent<HealthController>();
        }
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(animator!=null)
        {
            Look();
            Move();
            Strafe();
        }
        else
        {
            Debug.Log("SOMETHING WENT WRONG, I DON'T HAVE AN ANIMATOR");
        }
        Debug.Log("Grounded: " + grounded);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        //verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation += Mathf.Clamp(verticalLookRotation, -90f, 90f);
        // vertical axis for camera?
        //cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }

    void Move()
    {        
        Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        int animatorMoveParameter = (int)moveDir.z;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        animator.SetInteger("Move", 1);
        if (moveDir.z >= -0.1f && moveDir.z <= 0.1f)
        {
            animator.SetInteger("Move", 0);
        }
    }

    void Strafe()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
        //int animatorMoveParameter = (int)moveDir.x;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        animator.SetInteger("Move", 1);
        if (moveDir.x >= -0.1f && moveDir.x <= 0.1f)
        {
            //animator.SetInteger("Move", 0);
        }
    }

    void Jump()
    {
        /*
         * instantiate particles
        var emitParams = new ParticleSystem.EmitParams();
        jumpFx.Emit(emitParams, jumpParticlesBurst);
        */
        if (Input.GetAxisRaw("Jump") > 0 && grounded == true)
        {
            rb.AddForce(transform.up * jumpForce);

            animator.SetBool("Grounded", false);
        }
    }

    void Attack()
    {
        if (Input.GetAxisRaw("Attack") > 0 && animator.GetBool("Attacking") == false)
        {
            for (int i = 0; i < activeAbility.Length; i++)
            {
                activeAbility[i].Use();
            }
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
    }

    private void sprint()
    {
        //  Invoke("stopRollFx", .5f);
        Sequence sprintSequence = DOTween.Sequence();
        sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, sprintChargingSpeed, .3f).SetEase(Ease.OutQuad));
        sprintSequence.AppendCallback(() => { rollFx.Play(); });
        sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, sprintSpeed, sprintSpeedUpTime));
        sprintSequence.Append(DOTween.To(() => walkSpeed, x => walkSpeed = x, walkSpeed, sprintDuration).SetEase(Ease.OutQuad));
        sprintSequence.AppendCallback(() => { rollFx.Stop(); });
    }

    void FixedUpdate()
    {
        if (PV && !PV.IsMine)
            return;

        animator.SetBool("Grounded", grounded);
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

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        PhotonNetwork.LoadLevel(0);
        Destroy(gameObject);
    }


    private void stopSprint()
    {
        //movementSpeed = defaultSpeed;
    }

    private void stopRollFx()
    {
        rollFx.Stop();
    }

   
    public void Death()
    {   
        animator.SetBool("Death", true);       
    }


    public void Die()
    {
        PM.Die();
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        if (!PV.IsMine)
        {
            return;
        }

        Debug.Log("took damage" + damage);
    }

    [PunRPC]
    void RPC_Shoot()
    {
        if (!PV.IsMine)
        {
            return;
        }
    }
}