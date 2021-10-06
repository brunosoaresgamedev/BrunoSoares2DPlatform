using DG.Tweening;
using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knock : MonoBehaviour
{
    [SerializeField]
    float knockForce;

   
    public float knockTime;
    [SerializeField] public string otherTag;

    CharacterMovement2D characterMovement;
    CharacterFacing2D IsPlayerFacingRight;


    // Start isbo called before the first frame update]
    bool canKnockBack;
    void Start()
    {
        IsPlayerFacingRight = GetComponent<CharacterFacing2D>();
              characterMovement = GetComponent<CharacterMovement2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            CharacterFacing2D isTargetfacingRight = other.collider.GetComponent<CharacterFacing2D>();

            //  Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (isTargetfacingRight != null)
            {
                //  Vector3 difference = hit.transform.position - transform.position;
                // difference = difference.normalized * thrust;


                int dir = 1;
                if (isTargetfacingRight.IsFacingRight() == IsPlayerFacingRight.IsFacingRight())
                    dir = -1;
                else
                    dir = 1;
                if (!isTargetfacingRight.IsFacingRight() && IsPlayerFacingRight.IsFacingRight())
                    dir = -1;
                if(!isTargetfacingRight.IsFacingRight() && !IsPlayerFacingRight.IsFacingRight())
                    dir = 1;

                      characterMovement.currentVelocity.x =  dir * knockForce;

                    characterMovement.currentVelocity.y = knockForce / 5;
                
               


                //  Debug.Log(dir);


            }
        }
    }
   

}
