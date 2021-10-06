using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]
public class PassTroughPlatform : MonoBehaviour
{
    // private PlayerInput playerInput;
    [SerializeField]

    private PlatformEffector2D effector;
    public float waitTime = 0.1f;
   
    // Start is called before the first frame update
    void Start()
    {
  
        // playerInput = GetComponent<PlayerInput>();
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
       // if(playerInput.IsCrouchButtonUp())
       if(PlayerController.instancePlayer.isCrouch == false)
        {
            waitTime = 0f;
        }


        //     if (playerInput.IsCrouchButtonDown())
        if (PlayerController.instancePlayer.isCrouch == true)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.1f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (PlayerController.instancePlayer.isCrouch == false)
            effector.rotationalOffset = 0f;

        
    }

}
