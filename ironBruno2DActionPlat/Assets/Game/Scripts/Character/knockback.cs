using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    [SerializeField] public string otherTag;
    private Rigidbody2D rb;

    [SerializeField]
    PlayerHealthManager playerHealth;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    CharacterMovement2D characterMovement2D;
    [SerializeField]
    CharacterFacing2D facing2D;
    [SerializeField]
    GameObject DamageUIText;

    [SerializeField]
    Animator anim;

    [SerializeField]
    float knockbackForce;

    [SerializeField]
    float knockbackTime;

    [SerializeField]
    float knockbacktimer;

    bool canKnockBack;
    public bool knockBack;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealthManager>();
        playerController = GetComponent<PlayerController>();
        characterMovement2D = GetComponent<CharacterMovement2D>();
        facing2D = GetComponent<CharacterFacing2D>();
    }

    private void Update()
    {

       
        
    }
    
    public void Knockback()
    {
        DamageUIText.SetActive(true);
        spriteRenderer.color = new Color(1, 0.5f, 0.5f, 1);
        playerController.canControl = false;
        canKnockBack = false;
        playerController.PlayermovementInput.x = 0;
        rb.gravityScale = 0;
        characterMovement2D.StopImmediately();
        anim.SetTrigger("GetHit");
       gameObject.layer = 12;
       Invoke("ReleaseKnock", knockbacktimer);
        Invoke("ReleaseKnockBack", knockbackTime);
       Invoke("ReleaseDamageUI", 2);
    }
    

    void ReleaseKnock()
    {
       
        playerController.canControl = true;
        rb.gravityScale = 1;
    }
    void ReleaseDamageUI()
    {
        DamageUIText.SetActive(false);
    }
    void ReleaseKnockBack()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        
    }

    
}
