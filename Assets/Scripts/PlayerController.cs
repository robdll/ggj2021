using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent (typeof (HealthController))]
public class PlayerController : MonoBehaviour, IDamageable {
    
    //[SerializeField] GameObject cameraHolder;

    [SerializeField] Ability[] Abilities;

    int abilityIndex;
    int prevAbilityIndex = -1;

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
    PhotonView PV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            EquipAbility(0);
        }
        else { 
            // distruzione ignota di camere
               Destroy(GetComponentInChildren<Camera>().gameObject);
               Destroy(rb);
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

        /*
        for( int i=0; i<2; i++)
        {
            EquipAbility(i);
            break;
        }*/

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipAbility(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EquipAbility(0);
        }

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
        verticalLookRotation += Mathf.Clamp(verticalLookRotation, -90f, 90f);
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void EquipAbility(int _index)
    {
        if (_index == prevAbilityIndex)
            return;

        abilityIndex = _index;
        //switch item
        Abilities[abilityIndex].gameObject.SetActive(true);
        if (prevAbilityIndex != -1)
        {
            //hide prev item
            Abilities[prevAbilityIndex].gameObject.SetActive(false);
        }
        prevAbilityIndex = abilityIndex;

        
    }

        void Jump()
    {
        /*
        var emitParams = new ParticleSystem.EmitParams();
        jumpFx.Emit(emitParams, jumpParticlesBurst);
        */
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

    public void TakeDamage(float damage)
    {
        Debug.Log("took damage" + damage);
    }
}