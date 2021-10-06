using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    MinoEnemieAIController enemieAIController;
    public bool CanAttack;
    // Start is called before the first frame update
    void Start()
    {
        enemieAIController = GetComponent<MinoEnemieAIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
         //   enemieAIController.Attack();
        }
        else
        {
       //     enemieAIController.StopAttack();
        }
    }

    
}
