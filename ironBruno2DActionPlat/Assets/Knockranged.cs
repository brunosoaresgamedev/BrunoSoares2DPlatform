using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockranged : MonoBehaviour
{
    [SerializeField]
    float knockForce;

    bool IsAimRight;
    bool isAimUp;

    int contacts;
    public float thrust;
    public float knockTime;

    [SerializeField] public string otherTag;


    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    EnemyHealthManager enemyHealthManager1;

    CharacterMovement2D movement2D;

    bool canKnockBack;
    private void Awake()
    {
        GameObject thePlayer = GameObject.Find("/Character/Player");
        //  Debug.Log(thePlayer);
        playerInput = thePlayer.GetComponent<PlayerInput>();

        enemyHealthManager1 = GetComponent<EnemyHealthManager>();
        movement2D = GetComponent<CharacterMovement2D>();
    }
    void Start()
    {
        //  playerInput = GetComponent<PlayerInput>();




    }

    // Update is called once per frame
    void Update()
    {


    }



    //  public float maxAngle = 95;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            int dir = 1;
            if (!playerInput.IsAimRight)
                dir = -1;
            //  Debug.Log(dir);

          
            if(GameManager.instance.WeaponType == 0)
            {
                if(GameManager.instance.WeaponLevel0 >= 1)
                {
                    enemyHealthManager1.TakeDamage(2 * (GameManager.instance.WeaponLevel0));
                    movement2D.currentVelocity.x = dir * knockForce * (GameManager.instance.WeaponLevel0 / (GameManager.instance.WeaponLevel0 -1));
                }
                else
                {
                    enemyHealthManager1.TakeDamage(2);
                    movement2D.currentVelocity.x = dir * knockForce;
                }
       
            }
            if (GameManager.instance.WeaponType == 1)
            {
                if (GameManager.instance.WeaponLevel0 >= 1)
                {
                    enemyHealthManager1.TakeDamage(1 * (GameManager.instance.WeaponLevel0));
                   
                }
                else
                {
                    enemyHealthManager1.TakeDamage(1);
                }
            }
            enemyHealthManager1.applyDamage();
            movement2D.currentVelocity.y = knockForce / 5;
        }
        
    }

}
