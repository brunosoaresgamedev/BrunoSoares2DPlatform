using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCD : MonoBehaviour
{
    [Header("cooldown")]
    [Range(0.0f, 5f)]
    public float cooldownTime = 0.5f;
    public float nextFireTime = 0;
    [Header("cooldownsprite")]
    [Range(0.0f, 5f)]
    public float SpriteCDTime = 0.5f;
    PlayerInput playerInput;
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    public bool usingDash;
    public bool dashOver;



    bool isAttacking;
    bool AttackOver;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();

    }
    private void Update()
    {

        if (usingDash)
            StartCoroutine(usingDashFB());


        if (Time.time > nextFireTime)
        {
            if (playerInput.IsDashButtonDown())
            {

                spriteRenderer.color = new Color(0, 0, 0, 0.7f);
                gameObject.layer = 12;
                usingDash = true;
                playerController.UsingDash();
                nextFireTime = Time.time + cooldownTime;


            }
        }

        if (dashOver)
        {
            gameObject.layer = 9;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            dashOver = false;
        }


        if (dashOver)
        {
            //AfterAttack
        }
    }

    IEnumerator Attacking()
    {

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
        AttackOver = false;
        usingDash = false;
        dashOver = true;
    }

    IEnumerator usingDashFB()
    {

        yield return new WaitForSeconds(0.2f);

        usingDash = false;
        dashOver = true;
    }

}
