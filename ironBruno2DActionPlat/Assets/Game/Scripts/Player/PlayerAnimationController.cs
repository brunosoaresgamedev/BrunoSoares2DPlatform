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

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    CharacterMovement2D playerMovement;
    MinoEnemieAIController aIController;
    PlayerController playerController;
    private void Awake()
    {
        aIController = GetComponent<MinoEnemieAIController>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
        playerController = GetComponent<PlayerController>();

    }
    private void Update()
    {
        
        
        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, playerMovement.CurrentVelocity.x / playerMovement.MaxGroundSpeed);
        if (playerController != null)
        {


            animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, playerMovement.IsCrouching);
            animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, playerMovement.CurrentVelocity.y / playerMovement.JumpSpeed);
            animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, playerMovement.IsGrounded);
        }

        if(aIController != null)
        {
            animator.SetBool(EnemyAnimationKeys.IsChasing, aIController.isChasing);
        }
        
    }

    public void SetMeleeAttack()
    {
        animator.SetTrigger("Attack");
    }
}
