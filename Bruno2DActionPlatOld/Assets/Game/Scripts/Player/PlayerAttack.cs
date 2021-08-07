using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    public float attackRate = 0.5f;

    private PlayerController playerController;
    private PlayerAnimationController playerAnim;
    private PlayerInput playerInput;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<PlayerAnimationController>();
    }
    private void Awake()
    {
     

    }

    public void MeleeAttack()
    {
       
       

       
    }
   
    

    
}
