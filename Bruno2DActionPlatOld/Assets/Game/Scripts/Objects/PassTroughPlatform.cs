using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PassTroughPlatform : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlatformEffector2D effector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if(playerInput.IsCrouchButtonUp())
        {
            waitTime = 0f;
        }


        if (playerInput.IsCrouchButtonDown())
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (playerInput.IsjumpButtonHeld())
        {
            effector.rotationalOffset = 0f;
        }
    }

}
