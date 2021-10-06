using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlime : MonoBehaviour
{
    public enum State
    {
        Patrol,
        ChaseTarget,
        GoingBackToStart,
    }
    public State SlimeState;


    [SerializeField]
    private float AttackEnemyCoolDown;

    AIVision aIVision;
    CharacterFacing2D enemyFacing;
    CharacterMovement2D enemyMovement;
    [SerializeField]
    public Vector2 movementInput;
    TriggerManager triggerManager;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float targetMemoryDuration = 3f;
    [SerializeField]
    private GameObject target;
    // [SerializeField]
    //  private GameObject AttackHitBox;
    [SerializeField]
    private bool CanAttack;
    private float forgetTargetTime;
    private bool isAttacking;

    private bool isPatroling;
    public bool isChasing;
    [SerializeField]
    private float chaseSpeed;
    public bool isDead;
    bool isAfterAttack;

    [SerializeField] bool isJumping;
    [SerializeField] float ChaseJumpHeight;
    [SerializeField] float JumpSpeed;
    [SerializeField] float patrolJumpHeight;
    [SerializeField]
    EnemyHealthManager enemyHealthManager;
    public float patrolSpeed = 2f;
    [SerializeField]
    GameObject DeathFX;
    bool canMove;
    bool canDash;
    Rigidbody2D rigidbody2D;
    [SerializeField]
    float dashforce;
    [SerializeField]
    float dashTime;
    Vector2 toTarget;

    bool CurrentlyInJump;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        target = GameObject.FindGameObjectWithTag("Player");
        isPatroling = false;
        SlimeState = State.Patrol;
        triggerManager = GetComponent<TriggerManager>();
        anim = GetComponent<Animator>();
        aIVision = GetComponent<AIVision>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();
        enemyMovement.MaxGroundSpeed = JumpSpeed;

    }
    private void Awake()
    {
        isPatroling = false;
        SlimeState = State.Patrol;
        canDash = true;

    }

    // Update is called once per frame
    void Update()
    {



        if (!isDead && canMove)
        {
            enemyMovement.ProcessMovementInput(movementInput);
            enemyFacing.UpdateFacing(movementInput);
        }
        if (isDead)
        {
            DeathFX.SetActive(true);
            StopAllCoroutines();

            return;
        }


        switch (SlimeState)
        {
            default:
            case State.Patrol:

                StartPatrol();
                CheckVisibility();
                break;

            case State.ChaseTarget:

                ChaseTarget();
                StopPatrol();

                break;

            case State.GoingBackToStart:

                SlimeState = State.Patrol;

                break;
        }
        Jump();

    }

    public void ChaseTarget()
    {
        if (!CurrentlyInJump)
        {
            AttackEnemyCoolDown = 3;
            // enemyMovement.MaxGroundSpeed = chaseSpeed;

            toTarget = target.transform.position - transform.position;
            movementInput.x = Mathf.Sign(toTarget.x) * 0.01f;
        }


    }
    public bool CheckVisibility()
    {
        if (aIVision.IsVisible(target))
        {
            SlimeState = State.ChaseTarget;
            forgetTargetTime = Time.time + targetMemoryDuration;
            isChasing = true;
            return true;
        }

        return Time.time < forgetTargetTime;

    }

    private void StopPatrol()
    {
        StopCoroutine(Patrol());
        isPatroling = false;
    }
    private void StartPatrol()
    {

        if (!isPatroling)
        {
            enemyMovement.maxJumpHeight = patrolJumpHeight;
            //  AttackEnemyCoolDown = Random.Range(4, 8);
            // enemyMovement.MaxGroundSpeed = patrolSpeed;
            isPatroling = true;
            StartCoroutine(Patrol());
        }



    }
    public void Jump()
    {

        if (enemyMovement.IsGrounded)
        {
            anim.SetBool("isInAir", false);

            if (isJumping)
            {
                anim.SetBool("IsJumping", true);
            }

        }
        else
        {
            anim.SetBool("isInAir", true);
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasing)
                movementInput.x = 1;

            if (isChasing)
                movementInput.x = Mathf.Sign(toTarget.x);
            CurrentlyInJump = true;
            enemyMovement.maxJumpHeight = Random.Range(1.5f, 3.5f);
            Dash();
            enemyMovement.Jump();
            yield return new WaitForSeconds(1);
            movementInput.x = 0;
            CurrentlyInJump = false;
            yield return new WaitForSeconds(AttackEnemyCoolDown);

            if (!isChasing)
                movementInput.x = -1;

            if (isChasing)
                movementInput.x = Mathf.Sign(toTarget.x);
            CurrentlyInJump = true;
            enemyMovement.maxJumpHeight = Random.Range(1.5f, 3.5f);
            Dash();
            enemyMovement.Jump();
            yield return new WaitForSeconds(1);
            movementInput.x = 0;
            CurrentlyInJump = false;
            yield return new WaitForSeconds(AttackEnemyCoolDown);
        }


    }
    public void Dash()
    {

        if (canDash == true)
        {
            canMove = false;
            canDash = false;
            rigidbody2D.gravityScale = 0;
            //enemyMovement.StopImmediately();
            int dir = 1;
            if (!enemyFacing.IsFacingRight())
                dir = -1;
            enemyMovement.currentVelocity.x = dir * dashforce;
            Invoke("ReleaseDash", dashTime);

        }

    }
    void ReleaseDash()
    {
        canMove = true;
        canDash = false;
        rigidbody2D.gravityScale = 1;
    }

    public void Knock(Rigidbody2D rb, float knockTime)
    {
        StartCoroutine(KnockCo(rb, knockTime));
    }
    IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            SlimeState = State.ChaseTarget;
            rb.velocity = Vector2.zero;
        }
    }
}
