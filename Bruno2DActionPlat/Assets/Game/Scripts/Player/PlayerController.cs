using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(IDamageable))]
public class PlayerController : MonoBehaviour
{
    IDamageable damageable;

    [Header("Camera")]
    public Transform cameraTarget;
    [Range(0.0f, 5.0f)]
    public float cameraTargetOffsetX = 2.0f;
    [Range(0.5f, 50.0f)]
    public float cameraTargetFlipSpeed = 2.0f;
    [Range(0.0f, 5.0f)]
    public float characterSpeedInfluence = 2.0f;

    public bool canControl = true;
    public bool IsFlying = false;
    public bool wallGrab;
    public bool wallGrabAnim;
    public bool playerCanDash;

    PlayerCD playerCD;
    Rigidbody2D rb;
    CharacterFacing2D playerFacing;
    CharacterMovement2D playerMovement;
    PlayerInput playerInput;
    SpriteRenderer spriteRenderer;
    Collision collision;
    [SerializeField] GameObject weaponObject;
     public IWeapon Weapon { get; private set; }


    public Animator anim;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<Collision>();
        playerCD = GetComponent<PlayerCD>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<CharacterMovement2D>();
        playerInput = GetComponent<PlayerInput>();
        playerFacing = GetComponent<CharacterFacing2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<IDamageable>();
        damageable.OnDeath += OnDeath;

        if(weaponObject != null)
        {
            Weapon = weaponObject.GetComponent<IWeapon>();
        }
    }
    private void OnDestroy()
    {
        //note : remind to check if the thing you wanna destroy in not null
        if(damageable != null)
        {
            damageable.OnDeath -= OnDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
 
        if (!canControl)
        {
            return;
        }
        //movimento
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        playerFacing.UpdateFacing(movementInput);

    

        //pulo
        if (playerInput.IsJumpButtonDown())
        {
            playerMovement.Jump();
            
            
            
            if (Time.time > playerCD.nextFireTime)
            {
                if (playerMovement.IsGrounded == false && IsFlying == false)
                {
                    spriteRenderer.color = new Color(0, 0, 0, 0.7f);
                    gameObject.layer = 12;
                    playerCD.usingDash = true;
                    playerMovement.Dash();
                    playerCD.nextFireTime = Time.time + playerCD.cooldownTime;
                }
            }
            



        }
        if (playerInput.IsjumpButtonHeld() == true)
        {


        }
       


        if (playerInput.IsjumpButtonHeld() == false)
            {

                playerMovement.UpdateJumpAbort();
            }


            //agachamento

            if (playerInput.IsCrouchButtonDown())
            {

                //adicionar defesa (dano reduzido em 90%)
                playerMovement.Crouch();

            }
            if (playerInput.IsCrouchButtonUp())
            {
                playerMovement.UnCrouch();


            }

        if (Weapon != null && playerInput.IsAttackButtonDown())
        {
            Weapon.Attack();
        }

            //dash 
            /*
            if (collision.onWall && playerInput.IsJumpButtonDown())
            {
               
                wallGrab = true;
                
            }

            if (collision.onWall && playerInput.IsjumpButtonHeld() == false)
            {
                wallGrab = false;
            }
            
            if (!collision.onWall || playerInput.IsjumpButtonHeld() == false)
            {
                
                wallGrab = false;
            }

            if (wallGrab)
            {
                playerMovement.WallJump();
            }
            
            if (collision.onWall)
            {
               
                wallGrabAnim = true;
                
            }
            if (!collision.onWall)
            {
                
                wallGrabAnim = false;
            }
            
        
            
            anim.SetBool("GrabWall",wallGrabAnim);



            */
    }

    public void CanJump()
    {
        wallGrab = true;
    }
    public void CantJump()
    {
        wallGrab = false;
    }
    

  
        
        public void UsingDash()
        {
        if(playerCanDash)
        {
            if (playerInput.IsDashButtonDown())
            {
                playerMovement.Dash();
            }
          }
        }

    private void OnDeath()
    {
        playerMovement.canDash = false;
        playerCanDash = false;
        playerMovement.StopImmediately();
        enabled = false;
        
    }


}


