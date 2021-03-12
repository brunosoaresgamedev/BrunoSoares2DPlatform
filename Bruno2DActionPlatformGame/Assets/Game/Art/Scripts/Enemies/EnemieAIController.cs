using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
[RequireComponent(typeof(CharacterFacing2D))]
[RequireComponent(typeof(CharacterMovement2D))]
public class EnemieAIController : MonoBehaviour
{
    CharacterFacing2D enemyFacing;
    CharacterMovement2D enemyMovement;
    public Vector2 movementInput;


    // Start is called before the first frame update
    void Start()
    {
        enemyFacing = GetComponent<CharacterFacing2D>();
        enemyMovement = GetComponent<CharacterMovement2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);
    }

}
