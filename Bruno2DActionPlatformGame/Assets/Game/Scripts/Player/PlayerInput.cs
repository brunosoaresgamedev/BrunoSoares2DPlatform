using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
   
    private struct PlayerInputConstants
    {
        public const string Vertical = "Vertical";
        public const string Horizontal = "Horizontal";
        public const string Jump = "Jump";
        public const string Dash = "Dash";
       
    }
    private void Start()
    {
      
    }
    private void Update()
    {
        
    }
    public Vector2 GetMovementInput()
    {
         //input do teclado
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        //se o input for 0 ou ele ta parado ou mobile
        if(Mathf.Approximately(horizontalInput, 0.0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }

        return new Vector2(horizontalInput, 0);
    }

    public bool IsJumpButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    
    public bool IsjumpButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.Space);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);
        return isKeyboardButtonHeld || isMobileButtonHeld;
    }

    public bool IsDashButtonUp()
    {
        bool isKeyboardButtonUp = Input.GetKeyDown(KeyCode.J);
        bool isMobileButtonUp = CrossPlatformInputManager.GetButton(PlayerInputConstants.Dash);
        return isKeyboardButtonUp || isMobileButtonUp;
    }

    public bool IsDashButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.J);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Dash);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsDashButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.J);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Dash);
        return isKeyboardButtonHeld || isMobileButtonHeld;
    }




    public bool IsCrouchButtonDown()
    {
       
        bool isKeyboardButtonDown = Input.GetKey(KeyCode.S);
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < -0.5;
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsCrouchButtonUp()
    {
        bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;
        bool isMobileButtonUp = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) >= -0.5;
        return isKeyboardButtonUp && isMobileButtonUp;
    }
}
