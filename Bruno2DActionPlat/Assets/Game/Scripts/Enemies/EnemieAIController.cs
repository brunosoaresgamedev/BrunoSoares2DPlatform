using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
public class EnemieAIController : MonoBehaviour
{
    AIVision aIVision;
    CharacterFacing2D enemyFacing;
    CharacterMovement2D enemyMovement;
    public Vector2 movementInput;
    TriggerManager triggerManager;
    public Animator anim;

    public float targetMemoryDuration = 3f;
    public GameObject target;
    public GameObject AttackHitBox;
    public bool CanAttack;
    private float forgetTargetTime;
    private bool isAttacking;

    public bool isPatroling;
    public bool isChasing;
    public float chaseSpeed = 4f;
    public bool isDead;
    bool isAfterAttack;
    public float patrolSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        triggerManager = GetComponent<TriggerManager>();
        anim = GetComponent<Animator>();
        aIVision = GetComponent<AIVision>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();
       
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            enemyMovement.ProcessMovementInput(movementInput);
            enemyFacing.UpdateFacing(movementInput);

            StartPatrol();
            if (!isAfterAttack)
            {
                if (CheckVisibility())
                {
                    
                    ChaseTarget();
                    StopPatrol();
                    if (isChasing && aIVision.IsTargetUp)
                    {
                        enemyMovement.Jump();
                    }
                    else
                    {
                        StopPatrol();
                        ChaseTarget();
                    }

                }
                else
                {
                    isChasing = false;
                }

                if (isChasing == false)
                {
                    StartPatrol();
                }
                if (isChasing == true)
                {
                    StopPatrol();
                }

            }
            if (CanAttack)
            {
                StopPatrol();
                Attack();
            }
            else
            {
                StopAttack();
                
            }
        }
       


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isAttacking = false;
        }
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(attack());
    }
    public void StopAttack()
    {
        StopCoroutine(attack());
    }

    IEnumerator attack()
    {
        enemyMovement.MaxGroundSpeed = 1;
        isAttacking = true;
        anim.SetTrigger("IsAttacking");
        AttackHitBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AttackHitBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        enemyMovement.MaxGroundSpeed = patrolSpeed;
        isAttacking = false;


    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            CanAttack = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CanAttack = false;
        }
    }
    public void ChaseTarget()
    {

        enemyMovement.MaxGroundSpeed = chaseSpeed;
        Vector2 toTarget = target.transform.position - transform.position;
        movementInput.x = Mathf.Sign(toTarget.x);
    }
    public bool CheckVisibility()
    {
        if (aIVision.IsVisible(target))
        {
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
            enemyMovement.MaxGroundSpeed = patrolSpeed;
            isPatroling = true;
            StartCoroutine(Patrol());
        }



    }


    IEnumerator Patrol()
    {
        while (true)
        {
            movementInput.x = 1;
            yield return new WaitForSeconds(1.0f);
            movementInput.x = 0;
            yield return new WaitForSeconds(2.0f);
            movementInput.x = -1;
            yield return new WaitForSeconds(1.0f);
            movementInput.x = 0;
            yield return new WaitForSeconds(2.0f);
        }
    }

}
