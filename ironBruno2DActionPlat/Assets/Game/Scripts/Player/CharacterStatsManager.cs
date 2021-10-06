using Platformer2D.Character;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    XpManager xpManager;
    public int StatsPoint;
    PlayerHealthManager playerHealthManager;
    PlayerStaminaManager playerStaminaManager;
    public TextMeshProUGUI currentPointText;
    CharacterMovement2D characterMovement2D;
    [SerializeField]
    private float runspeed = 7.5f;

    [SerializeField]
    GameObject UIHasLVLpoint;
    bool inON;
    private void Awake()
    {
        xpManager = GetComponent<XpManager>();
        characterMovement2D = GetComponent<CharacterMovement2D>();
    }
    private void Start()
    {
        if(xpManager.level == 1)
        {
            StatsPoint = 3;
        }
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }
    private void FixedUpdate()
    {
        currentPointText.text = StatsPoint.ToString();
        if(StatsPoint > 0)
        {
            if(!inON)
            StartCoroutine(HasLVLPoints());
        }
        else
        {
            inON = false;
            
        }
       
    }
    IEnumerator HasLVLPoints()
    {
        while (true)
        {
            inON = true;
            UIHasLVLpoint.SetActive(true);
            yield return new WaitForSeconds(3);
            UIHasLVLpoint.SetActive(false);
            yield return new WaitForSeconds(3);
        }
        

    }
    public void Health()
    {
        if(StatsPoint > 0)
        {
            playerHealthManager.startingHealth += 25 * (xpManager.level / 2);
            StatsPoint--;
        }
        

    }
    public void Stamina()
    {
        if (StatsPoint > 0)
        {
            playerStaminaManager.startingStamina += 10 * (xpManager.level / 2);
            StatsPoint--;
        }
    }
    public void Aura()
    {
        if (StatsPoint > 0)
        { 

        }

    }

    public void Speed()
    {
        if (StatsPoint > 0)
        {
            characterMovement2D.MaxGroundSpeed += 1.5f/xpManager.level;
            StatsPoint--;
        }
    }

    public void JHump()
    {
        if (StatsPoint > 0)
        {
            characterMovement2D.maxJumpHeight += 1f / xpManager.level;
            StatsPoint--;
        }
    }

}
