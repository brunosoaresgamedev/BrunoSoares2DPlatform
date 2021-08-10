using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    EnemieAIController enemieAIController;
    public bool CanAttack;
    // Start is called before the first frame update
    void Start()
    {
        enemieAIController = GetComponent<EnemieAIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttack)
        {
            enemieAIController.Attack();
        }
        else
        {
            enemieAIController.StopAttack();
        }
    }

    
}
