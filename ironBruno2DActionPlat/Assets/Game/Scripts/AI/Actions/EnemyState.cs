using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    MinoEnemieAIController aIController;
    AIVision aIVision;

    public GameObject target;
    bool isvisible;

    private CharacterMovement2D charMovement;

    [SerializeField]
    private float chaseSpeed = 4f;

    private void Awake()
    {
        aIController = GetComponent<MinoEnemieAIController>();
        aIVision = GetComponent<AIVision>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isvisible = false;
        StartCoroutine(TEMP_Walk());
    }

    // Update is called once per frame
    void Update()
    {
        if (aIVision.IsVisible(target))
        {
            isvisible = true;
        }
    }

    
    void Chasetarget()
    {
        if(isvisible == true)
        {
            aIController.isChasing = true;
            charMovement.MaxGroundSpeed = chaseSpeed;
            Vector2 toTarget = target.transform.position - aIController.transform.position;
            aIController.movementInput.x = Mathf.Sign(toTarget.x);
        }
           
        
        
    }

   
    IEnumerator TEMP_Walk()
    {

        aIController.movementInput.x = 1;
        yield return new WaitForSeconds(1.0f);
        aIController.movementInput.x = 0;
        yield return new WaitForSeconds(2.0f);
        aIController.movementInput.x = -1;
        yield return new WaitForSeconds(1.0f);
        aIController.movementInput.x = 0;
        yield return new WaitForSeconds(2.0f);
    }
}


