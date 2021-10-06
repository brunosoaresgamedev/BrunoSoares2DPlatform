using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XpManager : MonoBehaviour
{
    CharacterStatsManager characterStats;
    [SerializeField]
    Text currentXPText, targetXPtext, levelText1, levelText2;
    [SerializeField] public int currentXP, targetXP, level;
    [SerializeField]
    private Image experienceBarImage;
    public static XpManager instance;
    PlayerHealthManager playerHealthManager;
    PlayerStaminaManager playerStaminaManager;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject LVLupFX;
    public AudioSource lvlupsoundEffect;
    [SerializeField]
    Transform healthBar;
    [SerializeField]
    bool islvlUp;
    [SerializeField]
   public bool hasLvlUp;
    [SerializeField]
    GameManager gameManager;


    GameObject canvasXp, canvasLvLShop;

    bool lvlup;
    private void Awake()
    {
        

        if (level <= 0)
        {
            level = 1;
        }
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        canvasXp = GameObject.Find("CanvasXP");
        canvasLvLShop = GameObject.Find("LvLMenu");
        levelText2 = canvasXp.transform.GetChild(0).GetComponent<Text>();

        currentXPText = canvasXp.transform.GetChild(0).GetComponent<Text>();
        levelText1 = canvasXp.transform.GetChild(1).GetComponent<Text>();
        targetXPtext = canvasXp.transform.GetChild(2).GetComponent<Text>();
        /*
                GameObject theTextCurrentXp = GameObject.Find("/Xpc");
                currentXPText = theTextCurrentXp.GetComponent<Text>();

                GameObject theTextTargetXp = GameObject.Find("/Xpc");
                targetXPtext = theTextTargetXp.GetComponent<Text>();

                GameObject theTextLevelXp = GameObject.Find("/Xpc");
                levelText1 = theTextLevelXp.GetComponent<Text>();

                GameObject theTextLevel2Xp = GameObject.Find("/Xpc");
                levelText2 = theTextLevel2Xp.GetComponent<Text>();

        */
        GameObject theGamemanager = GameObject.Find("/GameManager");
        gameManager = theGamemanager.GetComponent<GameManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        characterStats = GetComponent<CharacterStatsManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
       
        islvlUp = false;
       
       
        // gameManager = GetComponent<GameManager>();
        currentXPText.text = currentXP.ToString();
        targetXPtext.text = targetXP.ToString();
        levelText1.text = level.ToString();
        levelText2.text = level.ToString();
       
    }
    private void Update()
    {
        if (lvlup)
        {
            //  SetHeatlhBarSize(1.4f + level * .1f);
              SpawnParticleEffect();
              Flash();
           
          //  lvlupsoundEffect.Play();
            Debug.Log("lvlUp");

        }
        


         if (hasLvlUp)
        {
            SetValues();
        }
    }
    public void AddXP(int xp) {
        currentXP += xp;
        while(currentXP >= targetXP)
        {
            hasLvlUp = true;
            StartCoroutine(Leveling());
            characterStats.StatsPoint++;
            level++;
            targetXP += targetXP / 10 * level/3;
            levelText1.text = level.ToString();
            targetXPtext.text = targetXP.ToString();
            levelText2.text = level.ToString();
            playerHealthManager.currentHealth = playerHealthManager.startingHealth;
            playerStaminaManager.currentStamina = playerStaminaManager.startingStamina;
            currentXP = currentXP - targetXP;
        }


        if (currentXP < 0)
        {
            currentXP = 0;
        }
        StopCoroutine(Leveling());
        SetExperienceBarSize(GetExperienceNormalized());
        currentXPText.text = currentXP.ToString();
    }

    public void LoadSetValues()
    {
       if(gameManager != null)
        {
            currentXP = gameManager.currentXPAmount;
            targetXP = gameManager.targetXPAmount;
            level = gameManager.levelAmount;

        }


    }
    
    public void SetValues()
    {
        gameManager.currentXPAmount = currentXP;
        gameManager.targetXPAmount = targetXP;
        gameManager.levelAmount = level;
    }

    public float GetExperienceNormalized()
    {
        return (float)currentXP / targetXP;
    }
    public void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }
    public void SpawnParticleEffect()
    {
        StartCoroutine(ParticleEffectsTimer());
    }
    public void Flash()
    {
        StartCoroutine(ParticleEffectsTimerSprite());
    }
    IEnumerator ParticleEffectsTimerSprite()
    {
        spriteRenderer.color = new Color(1, 0, 1, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(0, 0, 1, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(0, 1, 0, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(0, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 0, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    IEnumerator Leveling()
    {
        lvlup = true;
       

        yield return new WaitForSeconds(0.2f);
        lvlup = false;
    }
    IEnumerator ParticleEffectsTimer()
    {
        LVLupFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        LVLupFX.SetActive(false);
    }
}
