using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{

    [SerializeField]
    private float AttackEnemyCoolDown;

  
    CharacterMovement2D enemyMovement;
    [SerializeField]
    public Vector2 movementInput;
  
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float targetMemoryDuration = 3f;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject AttackHitBox;
    [SerializeField]
    private bool CanAttack;

    private bool isAttacking;

    public bool isChasing;
  
    public bool isDead;



    public float patrolSpeed = 2f;
    [SerializeField]
    IsPlayerUp isPlayerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(AttackEnemyCoolDown);
        enemyMovement.MaxGroundSpeed = patrolSpeed;
        isAttacking = false;


    }
}
