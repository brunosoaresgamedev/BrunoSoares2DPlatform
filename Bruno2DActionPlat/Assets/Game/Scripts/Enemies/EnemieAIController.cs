using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(IDamageable))]
[RequireComponent(typeof(CharacterMovement2D))]
public class EnemieAIController : MonoBehaviour
{
    Animator anim;
    CharacterFacing2D enemyFacing;
    CharacterMovement2D enemyMovement;
    public Vector2 movementInput;
    IDamageable damageable;
    public bool isChasing;
    [SerializeField]
    private TriggerDamage damager;

    // Start is called before the first frame update
    void Start()
    {
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();
        damageable = GetComponent<IDamageable>();
        damageable.OnDeath += OnDeath;
    }

    public bool IsChasing
    {
        get => isChasing;
        set => isChasing = value;
    }

    private void OnDestroy()
    {
        if(damageable != null)
        {
            damageable.OnDeath -= OnDeath;
        }
    }
    public Vector2 MovementInput
    {
        get { return MovementInput; }
        set { movementInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1)); }
    }
    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);
    }

    private void OnDeath()
    {
        anim.SetTrigger("Dead");
        enemyMovement.StopImmediately();
        enabled = false;
        damager.gameObject.SetActive(false);
       
        Destroy(gameObject, 1f);
    }

}
