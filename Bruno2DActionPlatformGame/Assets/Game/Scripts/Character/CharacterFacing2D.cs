using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFacing2D : MonoBehaviour
{
    private PlayerInput playerInput;
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFacing(Vector2 movementInput)
    { 
        
        if (!facingRight && movementInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && movementInput.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }
}
