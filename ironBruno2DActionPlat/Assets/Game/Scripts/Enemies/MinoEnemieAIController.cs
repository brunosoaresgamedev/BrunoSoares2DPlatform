using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
public class MinoEnemieAIController : MonoBehaviour
{
    private enum State
    {
        Patrol,
        ChaseTarget,
        GoingBackToStart,
    }
    private State state;


    [SerializeField]
    private float Attack1EnemyCoolDown;
    [SerializeField]
    private float Attack2EnemyCoolDown;
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
    [SerializeField]
    private GameObject AttackHitBox;
    [SerializeField]
    private GameObject canAttackHitBox;
    [SerializeField]
    private GameObject AttackHitBox2;
    [SerializeField]
    private bool CanAttack;
    private float forgetTargetTime;
    private bool isAttacking;
    private bool isAttacking2;

    private bool isPatroling;
    public bool isChasing;
    private  float chaseSpeed = 4f;
    private float AttackSpeed = 0;
    public bool isDead;
 //   bool isAfterAttack;
    bool canControl;
    CharacterFacing2D facing2D;
    [SerializeField]
   EnemyHealthManager enemyHealthManager;
    public float patrolSpeed = 2f;
    [SerializeField]
    IsPlayerUp isPlayerUp;
    [SerializeField]
    IsPlayerForward isPlayerForward;
    Rigidbody2D rigidbody2d;
    //  Rigidbody2D rigidbody2d;
    //  int chooseattk;
    //  CharacterFacing2D facing2D;
    [SerializeField]
    float dashForce;
    [SerializeField]
    float dashTime;
    [SerializeField]
    bool CanDash;
    [SerializeField]
    Canattack canattack;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //  canattack = GetComponent<Canattack>();
        facing2D = GetComponent<CharacterFacing2D>();
       rigidbody2d = GetComponent<Rigidbody2D>();
        // AttackEnemyCoolDown = Random.Range(0.1f, 3);
        target = GameObject.FindGameObjectWithTag("Player");
       
      
       
        isPatroling = false;
        state = State.Patrol;
        triggerManager = GetComponent<TriggerManager>();
        anim = GetComponent<Animator>();
        aIVision = GetComponent<AIVision>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();

    }
    private void Awake()
    {
        CanDash = true;
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        isPatroling = false;
        state = State.Patrol;
        canControl = true;
    }

    // Update is called once per frame
    void Update()
    {
 
        if (!isDead && canControl)
        {
            enemyMovement.ProcessMovementInput(movementInput);
            enemyFacing.UpdateFacing(movementInput);
        }
        if (isDead)
            return;

        switch (state)
        {
            default:
            case State.Patrol:

               

                StartPatrol();
                CheckVisibility();
                break;
                
            case State.ChaseTarget:

               

                ChaseTarget();

                if (isPlayerForward.Isforward && CanDash)
                {
                    Attack2();
                }
                if (canattack.CanAttackPlayer)
                {
                    Attack1();        
                }

               

                if (isPlayerUp.IsUp)
                {
                    enemyMovement.Jump();
                }
                if (!CheckVisibility())
                {
                    state = State.GoingBackToStart;
                }
                else
                {
                    state = State.ChaseTarget;
                    
                }
                break;
            case State.GoingBackToStart:

                state = State.Patrol;

                break;
        }


}
private void OnTriggerExit(Collider other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    {
        isAttacking = false;
    }
}

public void Attack1()
    {
        if (!isAttacking)
            anim.SetTrigger("IsAttacking1");

}
    public void Attack2()
    {
        if (!isAttacking)
        {
            spriteRenderer.color = new Color(0.5f, 0, 0, 1);
            enemyMovement.MaxGroundSpeed = 0;
            anim.SetTrigger("IsAttacking2");
        }
     
        

        //  if (!isAttacking)
        //    StartCoroutine(attack());
    }


    public void StopAttack()
{
  //  StopCoroutine(attack());
}

    public void setdashtotrue()
    {
        canAttackHitBox.SetActive(false);
        
    }
    public void setattacktotrue()
    {
        
        CanDash = false;
        isAttacking = true;
        enemyMovement.MaxGroundSpeed = 0;
    }
    public void HitBoxActive()
    {

        isAttacking = true;
        enemyMovement.MaxGroundSpeed = AttackSpeed;
        AttackHitBox.SetActive(true);
    }
    public void HitBoxDesactive()
    {
       // isAttacking = false;
        AttackHitBox.SetActive(false);
        enemyMovement.MaxGroundSpeed = 5;
       // Invoke("StopAttackDelay", 0.2f);
        Invoke("attackCoolDown", Attack1EnemyCoolDown);
    }
    public void HitBox2Active()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        isAttacking = true;
        enemyMovement.MaxGroundSpeed = 2.5f;
        AttackHitBox2.SetActive(true);

        // StartCoroutine(StopAttackDelay());
    }
    public void HitBox2Desactive()
    {
        isAttacking = false;
        enemyMovement.MaxGroundSpeed = AttackSpeed;
        AttackHitBox2.SetActive(false);
        Invoke("StopAttackDelay", 1);
        Invoke("DashCoolDown", Attack2EnemyCoolDown);

    }

    private void attackCoolDown()
    {
        isAttacking = false;
        CanAttack = true;
    }

    private void DashCoolDown()
    {
        canAttackHitBox.SetActive(true);
        CanDash = true;
    
    }

    void StopAttackDelay()
    {
        enemyMovement.MaxGroundSpeed = chaseSpeed;
    }

public void ChaseTarget()
{
       // StopCoroutine(StopAttackDelay());
    //enemyMovement.MaxGroundSpeed = chaseSpeed;
    Vector2 toTarget = target.transform.position - transform.position;
    movementInput.x = Mathf.Sign(toTarget.x);
}
public bool CheckVisibility()
{
    if (aIVision.IsVisible(target))
    {
        state = State.ChaseTarget;
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
        yield return new WaitForSeconds(Random.Range(1, 2));
        movementInput.x = 0;
        yield return new WaitForSeconds(Random.Range(2, 3));
        movementInput.x = -1;
        yield return new WaitForSeconds(Random.Range(1, 2));
        movementInput.x = 0;
        yield return new WaitForSeconds(Random.Range(2, 3));
    }
}

}


