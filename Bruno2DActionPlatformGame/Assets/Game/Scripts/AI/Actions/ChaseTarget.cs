using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
   [InParam("Target")]
    private GameObject target;
    [InParam("AIControl")]
    private EnemieAIController AIController;

    [InParam("ChaseSpeed")]
    private float chaseSpeed;

    [InParam("CharacterMovement")]
    private CharacterMovement2D charMovement;
    public override void OnStart()
    {
        base.OnStart();
        AIController.isChasing = true;
        charMovement.MaxGroundSpeed = chaseSpeed;
    }

    public override void OnAbort()
    {
        base.OnAbort();
        AIController.isChasing = false;
    }
    public override TaskStatus OnUpdate()
    {
        Vector2 toTarget = target.transform.position - AIController.transform.position;
        AIController.movementInput.x = Mathf.Sign(toTarget.x);
        return TaskStatus.RUNNING;
        
    }
}
