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

    public float targetMemoryDuration = 3f;
    public GameObject target;

    private float forgetTargetTime;

    public bool isPatroling;
    public bool isChasing;
    public float chaseSpeed = 4f;

    public float patrolSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        aIVision = GetComponent<AIVision>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);

        if (!isChasing)
        {
            StartPatrol();
        }

        if (CheckVisibility())
        {
            
            StopPatrol();
            Debug.Log("ChaseTarget");
            ChaseTarget();
        }
        else
        {
            StartPatrol();
        }
        
    }
    
      public void ChaseTarget()
    {
        isChasing = true;
        enemyMovement.MaxGroundSpeed = chaseSpeed;
        Vector2 toTarget = target.transform.position - transform.position;
        movementInput.x = Mathf.Sign(toTarget.x);
    }
       public bool CheckVisibility()
    {
        if (aIVision.IsVisible(target))
        {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        isChasing = false;
        return Time.time < forgetTargetTime;
      
    }

    private void StopPatrol()
    {
        StopAllCoroutines();
        isPatroling = false;
    }
    public void StartPatrol()
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
