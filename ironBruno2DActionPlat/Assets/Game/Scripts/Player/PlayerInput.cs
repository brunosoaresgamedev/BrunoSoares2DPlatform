using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instancePlayerInput;
    GameManager gameManager;
    [SerializeField]
    public bool IsAimRight;
    [SerializeField]
    public bool IsAimUp;

    [SerializeField]
    public bool IsFaceRight;
    [SerializeField]
    public bool IsFaceUp;
    [SerializeField]
    public bool CanFlip;

    [SerializeField]
    public bool IsAiming;

    [SerializeField]
    GameObject AimJoystickObject;

    [SerializeField]
    GameObject JoystickObject;

    [SerializeField]
    GameObject ButtonObject;

    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    public bool IsUsingButtons;

    [SerializeField]
    public bool IsUsingJoystick;

    [SerializeField]
    public bool IsUsingTouchAny;

    [SerializeField]
    public bool IsUsingJoystickAim;



    public void ChangeForJoystick()
    {
        IsUsingButtons = false;
        IsUsingJoystick = true;

        JoystickObject.SetActive(true);
        ButtonObject.SetActive(false);
    }


    public void ChangeForButtons()
    {
        IsUsingButtons = true;
        IsUsingJoystick = false;

        JoystickObject.SetActive(false);
        ButtonObject.SetActive(true);
    }

    public void ChangeForJoystickAim()
    {
        IsUsingTouchAny = false;
        IsUsingJoystickAim = true;

        AimJoystickObject.SetActive(true);

    }
    public void ChangeForTouchAny()
    {
        IsUsingTouchAny = true;
        IsUsingJoystickAim = false;

        AimJoystickObject.SetActive(false);
    }




    public bool isMoving;
    private struct PlayerInputConstants
    {
        public const string HorizontalA = "HorizontalA";
        public const string VerticalA = "VerticalA";
        public const string Horizontal = "Horizontal";
        public const string Horizontalp = "Horizontalp";
        public const string Horizontaln = "Horizontaln";
        public const string Vertical = "Vertical";
        public const string VerticalC = "VerticalC";
        public const string Jump = "Jump";
        public const string Dash = "Dash";
        public const string Attack = "Attack";
        public const string RangeAttack = "RangeAttack";
    }
    private void Awake()
    {
        if (instancePlayerInput == null)
        {
            instancePlayerInput = this;
            //  DontDestroyOnLoad(gameObject);
        }
        //else if (instancePlayerInput != this)
        // {
        //  Destroy(gameObject);
        //  }
    }

    private void Start()
    {
        IsUsingTouchAny = false;
        IsUsingJoystickAim = true;


        IsUsingJoystick = true;
        IsUsingButtons = false;
    }
    private void Update()
    {

    }
    public Vector2 GetHorizontalMovementInput()
    {

        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        if (!IsUsingButtons && IsUsingJoystick)
        {
            horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
            //se o input for 0 ou ele ta parado ou mobile
            if (Mathf.Approximately(horizontalInput, 0.0f))
            {
                //Debug.Log(isMoving);

                horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
            }
            return new Vector2(horizontalInput, 0);
        }
        return new Vector2(horizontalInput, 0);
    }
    public Vector2 GetVerticalAndHorizontalMovementInput()
    {

        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
        float verticallInput = Input.GetAxisRaw(PlayerInputConstants.Vertical);

        if (!IsUsingButtons && IsUsingJoystick)
        {
            horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
            verticallInput = Input.GetAxisRaw(PlayerInputConstants.Vertical);

            //se o input for 0 ou ele ta parado ou mobile
            if (Mathf.Approximately(horizontalInput, verticallInput))
            {

                verticallInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical);
                horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);


                if (horizontalInput == 0)
                {
                    CanFlip = false;
                }
                else CanFlip = true;
                if (horizontalInput > 0)
                    IsFaceRight = true;
                else
                    IsFaceRight = false;


            }
            return new Vector2(horizontalInput, verticallInput);
        }
        return new Vector2(horizontalInput, verticallInput);
    }

    public Vector2 GetAttackInput()
    {

        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.HorizontalA);
        float verticallInput = Input.GetAxisRaw(PlayerInputConstants.VerticalA);

        if (!IsUsingButtons && IsUsingJoystick)
        {
            horizontalInput = Input.GetAxisRaw(PlayerInputConstants.HorizontalA);
            verticallInput = Input.GetAxisRaw(PlayerInputConstants.VerticalA);

            //se o input for 0 ou ele ta parado ou mobile
            if (Mathf.Approximately(horizontalInput, verticallInput))
            {

                //Debug.Log(isMoving);
                isMoving = true;
                verticallInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.VerticalA);
                horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.HorizontalA);
                //  Debug.Log(horizontalInput);
                // Debug.Log(verticallInput);
                // Debug.Log(horizontalInput);
                if (verticallInput > 0)
                    IsAimUp = true;
                else
                    IsAimUp = false;
                // Debug.Log(IsAimUp);

                if (horizontalInput > 0)
                    IsAimRight = true;
                else
                    IsAimRight = false;
                //    Debug.Log(IsAimRight);


                if (verticallInput != 0 || horizontalInput != 0)
                    IsAiming = true;
                else
                    IsAiming = false;
            }
            return new Vector2(horizontalInput, verticallInput);
        }
        return new Vector2(horizontalInput, verticallInput);



        if (!IsUsingButtons && IsUsingJoystick)
        {
            verticallInput = Input.GetAxisRaw(PlayerInputConstants.VerticalA);
            //se o input for 0 ou ele ta parado ou mobile
            if (Mathf.Approximately(verticallInput, 0.0f))
            {
                //Debug.Log(isMoving);
                verticallInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.VerticalA);
                //    Debug.Log(verticallInput);

                if (verticallInput > 0 || horizontalInput > 0)
                    IsAiming = true;
                else
                    IsAiming = false;

            }
            return new Vector2(verticallInput, 0);
        }
        return new Vector2(verticallInput, 0);
    }



    public bool IsLeftButtonDown()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {

            bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.A);
            bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Horizontaln);
            return isKeyboardButtonDown || isMobileButtonDown;
        }
        return false;
    }

    public bool IsLeftButtonUp()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {

            bool isKeyboardButtonUp = Input.GetKeyUp(KeyCode.A);
            bool isMobileButtonUp = CrossPlatformInputManager.GetButtonUp(PlayerInputConstants.Horizontaln);
            return isKeyboardButtonUp || isMobileButtonUp;
        }
        return false;
    }
    public bool IsRightButtonDown()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.D);
            bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Horizontalp);
            return isKeyboardButtonDown || isMobileButtonDown;
        }
        return false;
    }

    public bool IsRightButtonUp()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonUp = Input.GetKeyUp(KeyCode.D);
            bool isMobileButtonUp = CrossPlatformInputManager.GetButtonUp(PlayerInputConstants.Horizontalp);
            return isKeyboardButtonUp || isMobileButtonUp;
        }
        return false;
    }

    public bool IsRightButtonHeld()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonHeld = Input.GetKey(KeyCode.D);
            bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Horizontalp);
            return isKeyboardButtonHeld || isMobileButtonHeld;
        }
        return false;
    }

    public bool IsLeftButtonHeld()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonHeld = Input.GetKey(KeyCode.A);
            bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Horizontaln);
            return isKeyboardButtonHeld || isMobileButtonHeld;
        }
        return false;
    }

    public bool IsCrouchButtonDown()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonDown = Input.GetKey(KeyCode.S);

            bool isMobileButtonDown = CrossPlatformInputManager.GetButton(PlayerInputConstants.VerticalC);
            return isKeyboardButtonDown || isMobileButtonDown;
        }
        if (IsUsingJoystick && !IsUsingButtons)
        {
            bool isKeyboardButtonDown = Input.GetKey(KeyCode.S);
            bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < -0.5;
            return isKeyboardButtonDown || isMobileButtonDown;
        }


        return false;
    }
    public bool IsCrouchButtonUp()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {
            bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;

            bool isMobileButtonUp = CrossPlatformInputManager.GetButton(PlayerInputConstants.VerticalC) == false;
            return isKeyboardButtonUp && isMobileButtonUp;
        }
        if (IsUsingJoystick && !IsUsingButtons)
        {
            bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;
            bool isMobileButtonUp = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) >= -0.5;
            return isKeyboardButtonUp && isMobileButtonUp;
        }
        return false;
    }
    public bool IsCrouchButtonHeld()
    {
        if (!IsUsingJoystick && IsUsingButtons)
        {

            bool isKeyboardButtonHeld = Input.GetKey(KeyCode.S);
            bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.VerticalC);
            return isKeyboardButtonHeld || isMobileButtonHeld;

        }
        return false;
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





    public bool IsAttackButtonUp()
    {
        bool isKeyboardButtonUp = Input.GetKeyUp(KeyCode.K);
        bool isMobileButtonUp = CrossPlatformInputManager.GetButtonUp(PlayerInputConstants.Attack);
        return isKeyboardButtonUp || isMobileButtonUp;
    }

    public bool IsAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.K);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsRangeAttackButtonUp()
    {
        bool isKeyboardButtonUp = Input.GetKeyUp(KeyCode.I);
        bool isMobileButtonUp = CrossPlatformInputManager.GetButtonUp(PlayerInputConstants.RangeAttack);
        return isKeyboardButtonUp || isMobileButtonUp;
    }

    public bool IsRangeAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.I);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.RangeAttack);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
    public bool IsRangeAttackButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.I);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.RangeAttack);
        return isKeyboardButtonHeld || isMobileButtonHeld;
    }
    public bool IsAttackButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.K);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Attack);
        return isKeyboardButtonHeld || isMobileButtonHeld;
    }
}
