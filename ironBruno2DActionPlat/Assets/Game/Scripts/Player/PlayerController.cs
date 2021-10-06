using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using CodeMonkey.Utils;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{



    public bool canAttack;
    public bool IsAttacking;



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
    public bool IsMovingController;

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
    public bool isCrouch;
    PlayerAimWeapon playerAimWeapon;
    public static PlayerController instancePlayer;
    [SerializeField]
    public Vector2 PlayermovementInput;

    [SerializeField]
    public float DashCooldown;

    [SerializeField]
    GameObject DashButtonFeedback;

    public Animator anim;


    public Image cooldown;
    public bool coolingDown;


    GameObject boat;

    private void Awake()
    {
        if (instancePlayer == null)
        {
            instancePlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        //  else if (instancePlayer != this)
        //  {
        //Destroy(gameObject);
        // }

        
    }
    // Start is called before the first frame update
    void Start()
    {

        playerAimWeapon = GetComponent<PlayerAimWeapon>();
        playerAnim = GetComponent<PlayerAnimationController>();
        collision = GetComponent<Collision>();
        playerCD = GetComponent<PlayerCD>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<CharacterMovement2D>();
        playerInput = GetComponent<PlayerInput>();
        playerFacing = GetComponent<CharacterFacing2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        canAttack = true;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

       
      


        if (!canControl)
        {
            return;
        }
        if (playerInput.IsUsingButtons && !playerInput.IsUsingJoystick)
        {
            playerAimWeapon.UpdateIfCanAim(PlayermovementInput);
            playerFacing.UpdateFacing(PlayermovementInput);
            playerMovement.ProcessMovementInput(PlayermovementInput);
        }

        //movimento

        if (playerInput.IsUsingJoystick && playerInput.IsUsingJoystick)
        {
            Vector2 movementInput = playerInput.GetHorizontalMovementInput();
            playerMovement.ProcessMovementInput(movementInput);

            playerAimWeapon.UpdateIfCanAim(movementInput);
            playerFacing.UpdateFacing(movementInput);
        }
        //  Vector2 movementInput = playerInput.GetMovementInput();

        if (playerInput.IsLeftButtonUp())
        {
            PlayermovementInput.x = 0;
        }
        if (playerInput.IsRightButtonUp())
        {
            PlayermovementInput.x = 0;
        }
        if (playerInput.IsLeftButtonDown())
        {
            PlayermovementInput.x -= 1;
        }

        if (playerInput.IsRightButtonDown())
        {
            PlayermovementInput.x += 1;
        }


        //pulo
        if (playerInput.IsJumpButtonDown())
        {
            playerMovement.Jump();


/*
            if (Time.time > playerCD.nextFireTime)
            {
                if (playerMovement.IsGrounded == false && IsFlying == false)
                {
                    spriteRenderer.color = new Color(0, 0, 0, 0.7f);
                    // gameObject.layer = 12;
                    playerCD.usingDash = true;
                    playerMovement.Dash();
                    playerCD.nextFireTime = Time.time + playerCD.cooldownTime;
                }
            }
*/



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

            if (canAttack)
            {

                coolingDown = true;
                //   gameObject.tag = "Attack";
                //   gameObject.layer = 13;
                AttackTrigger.SetActive(true);
                anim.SetTrigger("Attack");
                DashAttack();
            }
            //  


        }

        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            cooldown.fillAmount += 0.2f / (DashCooldown / 5) * Time.deltaTime;
        }
        if (coolingDown == false)
        {
            cooldown.fillAmount = 0;
        }
    }

    public void DashAttack()
    {
        IsAttacking = true;
        StartCoroutine(StopCooldownAttack());
        cooldown.fillAmount = 0;
        DashButtonFeedback.SetActive(true);
        canAttack = false;

        playerCD.usingDash = true;

        canControl = false;
        playerMovement.StopImmediately();
        playerMovement.Dash();
    }
    /*  public void StopAttack()
      {

          canControl = true;
          AttackTrigger.SetActive(false);
          Invoke("CanAttack", DashCooldown);
      }*/
    IEnumerator StopCooldownAttack()
    {
        //PlayermovementInput.x = 0;
        //anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
        //  gameObject.tag = "Player";
        // gameObject.layer = 9;
        AttackTrigger.SetActive(false);
        yield return new WaitForSeconds(DashCooldown);

        DashButtonFeedback.SetActive(false);
        coolingDown = false;
        canAttack = true;
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



    public void UsingDash()
    {
        if (playerInput.IsDashButtonDown())
        {
            playerMovement.Dash();
        }
    }


    public void Knock(Rigidbody2D rb, float knockTime)
    {
        StartCoroutine(KnockCo(rb, knockTime));
    }


    IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            canControl = false;
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;

        }
    }

}


