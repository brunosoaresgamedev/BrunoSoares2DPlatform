using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{







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

    public GameObject AttackTrigger;
    public GameObject bullet;
    public Transform bulletPoint;

    PlayerCD playerCD;
    PlayerAnimationController playerAnim;
    Rigidbody2D rb;
    CharacterFacing2D playerFacing;
    CharacterMovement2D playerMovement;
    PlayerInput playerInput;
    SpriteRenderer spriteRenderer;
    Collision collision;
    bool isCrouch;


    public Animator anim;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

        playerAnim = GetComponent<PlayerAnimationController>();
        collision = GetComponent<Collision>();
        playerCD = GetComponent<PlayerCD>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<CharacterMovement2D>();
        playerInput = GetComponent<PlayerInput>();
        playerFacing = GetComponent<CharacterFacing2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();



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
            if (canControl)
            {
                isCrouch = true;
                //adicionar defesa (dano reduzido em 90%)
                playerMovement.Crouch();
            }


        }
        if (playerInput.IsCrouchButtonUp())
        {
            if (canControl)
            {
                isCrouch = false;
                playerMovement.UnCrouch();

            }


        }




        if (playerInput.IsAttackButtonDown())
        {


            StartCoroutine(Attack());


        }


    }


    IEnumerator Attack()
    {

        gameObject.layer = 12;
        playerCD.usingDash = true;
        AttackTrigger.SetActive(true);
        canControl = false;
        playerMovement.StopImmediately();
        anim.SetTrigger("Attack");
        playerMovement.Dash();
        yield return new WaitForSeconds(0.5f);
        canControl = true;

        AttackTrigger.SetActive(false);


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
         
            playerMovement.RigidBody.velocity = new Vector2(playerMovement.RigidBody.velocity.x, playerMovement.JumpSpeed * 0.75f);

        }
    }

    public void CanJump()
    {
        wallGrab = true;
    }
    public void CantJump()
    {
        wallGrab = false;
    }


    void dashFeedback()
    {


    }

    public void UsingDash()
    {
        if (playerInput.IsDashButtonDown())
        {
            playerMovement.Dash();
        }

    }



}


