using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoatScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject BoatPoint;

    [SerializeField]
    LvlMenuLeanTwen leanTwen;
    bool IsUsingBoat;
    SpriteRenderer spriteRender;
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        BoatPoint = GameObject.Find("BoatPoint");
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.transform.position = other.transform.position;
                
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
                
                PlayerController playerController = other.GetComponent<PlayerController>();
                PlayerInput playerInput = other.GetComponent<PlayerInput>();


                other.transform.position = BoatPoint.transform.position;

                playerController.canControl = false;

                Vector2 movementInputHV = playerInput.GetVerticalAndHorizontalMovementInput();

                rb.MovePosition(rb.position + movementInputHV * 5 * Time.fixedDeltaTime);

            if (!playerInput.IsFaceRight && playerInput.CanFlip)
                spriteRender.flipX=true;

            if (playerInput.IsFaceRight && playerInput.CanFlip)
                spriteRender.flipX = false;

        }      
    }

   
}
