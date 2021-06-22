using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

public static class CharacterMovementAnimationKeys
{
    public const string IsCrouching = "IsCrouching";
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";
}

public static class EnemyAnimationKeys
{
    public const string IsChasing = "IsChasing";
}

public class CharacterAnimationController : MonoBehaviour
{
    protected Animator animator;
    protected CharacterMovement2D characterMovement;

    EnemieAIController aIController;
   
    protected virtual void Awake()
    {
        aIController = GetComponent<EnemieAIController>();
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement2D>();
    }
    protected virtual void Update()
    {

        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, characterMovement.CurrentVelocity.x / characterMovement.MaxGroundSpeed);

        if (aIController != null)
        {
            animator.SetBool(EnemyAnimationKeys.IsChasing, aIController.isChasing);
        }
        
    }
}
