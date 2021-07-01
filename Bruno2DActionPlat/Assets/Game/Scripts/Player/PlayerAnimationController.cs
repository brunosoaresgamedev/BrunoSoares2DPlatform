using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CharacterFacing2D))]
public static class CharacterMovementAnimationKeys
{
    public const string IsCrouching = "IsCrouching";
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";
    public const string IsAttacking = "IsAttacking";

}

public static class EnemyAnimationKeys
{
    public const string IsChasing = "IsChasing";
}

public class PlayerAnimationController : MonoBehaviour
{

    private IDamageable damageable;

    public const string TriggerDead = "Dead";
    

    Animator animator;
    CharacterMovement2D playerMovement;
    EnemieAIController aIController;
    PlayerController playerController;
    private void Awake()
    {
        aIController = GetComponent<EnemieAIController>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
        playerController = GetComponent<PlayerController>();
        damageable = GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.OnDeath += OnDeath;
        }
    }

    private void OnDestroy()
    {
        if(damageable != null)
        {
            damageable.OnDeath -= OnDeath;
        }
    }
    private void Update()
    {
        
        
        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, playerMovement.CurrentVelocity.x / playerMovement.MaxGroundSpeed);
        if (playerController != null)
        {


            animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, playerMovement.IsCrouching);
            animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, playerMovement.CurrentVelocity.y / playerMovement.JumpSpeed);
            animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, playerMovement.IsGrounded);
            animator.SetBool(CharacterMovementAnimationKeys.IsAttacking, playerController.Weapon.IsAttacking);
        }

        if(aIController != null)
        {
            animator.SetBool(EnemyAnimationKeys.IsChasing, aIController.isChasing);
        }
        
    }

    private void OnDeath()
    {
        animator.SetTrigger(TriggerDead);
    }
}
