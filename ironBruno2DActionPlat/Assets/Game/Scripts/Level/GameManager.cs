using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Platformer2D.Character;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int WeaponLevel0, WeaponLevel1, WeaponLevel2;

    [SerializeField]
    public int WeaponType;





    PlayerStaminaManager playerStaminaManager;
   
    PlayerHealthManager playerHealthManager;
  
    XpManager xpManager;
 
    CharacterStatsManager CharacterStatsManager;
    public bool isEnemyDead;
    public TextMeshProUGUI textCoin,textOre,textMiner,textMinerMax;   
    public int coin;
    public int coinRecovery;
    public static GameManager instance;
    public Transform playerTransform;
   
    CharacterMovement2D characterMovement2D;
    bool IsRespawn;
    [SerializeField]
      public Transform SpawnPosition;

    [SerializeField]
    GameObject RespawnUICanvas, Coin;
    [SerializeField]
    Animator animUI;
    [SerializeField] public int currentXPAmount, targetXPAmount = 100, levelAmount;
    [SerializeField]
    bool CanResetLvl;


   public GameObject ThePlayer;
   public GameObject Boat;
    [SerializeField]
    public int IronOre;

    [SerializeField]
    GameObject CharacterPlayerPreFab;


    [SerializeField]
    public int maxMining =10;
    [SerializeField]
    public int CurrentMining;

    public int IsMinerDead;


    private void Awake()
    {
        Boat = GameObject.Find("boat");
        Coin = GameObject.Find("CanvasXP");
        textMinerMax = Coin.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
        textMiner = Coin.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
        textOre = Coin.transform.GetChild(9).GetComponent<TextMeshProUGUI>();
        textCoin = Coin.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        ThePlayer = GameObject.Find("Player");

       
      //  GameObject spawnTransform = GameObject.Find("/SpawnPoint");
        //SpawnPosition = spawnTransform.GetComponent<Transform>();

        ThePlayer = GameObject.Find("/Character/Player");

        playerTransform = ThePlayer.GetComponent<Transform>();
        
        playerStaminaManager = ThePlayer.GetComponent<PlayerStaminaManager>();
        playerHealthManager = ThePlayer.GetComponent<PlayerHealthManager>();
        xpManager = ThePlayer.GetComponent<XpManager>();
        CharacterStatsManager = ThePlayer.GetComponent<CharacterStatsManager>();
        characterMovement2D = ThePlayer.GetComponent<CharacterMovement2D>();


        if (CanResetLvl)
            PlayerPrefs.DeleteAll();
        Load();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
      
      
    }

    public void RunAwake()
    {
        Boat = GameObject.Find("boat");
        Coin = GameObject.Find("CanvasXP");
        textMinerMax = Coin.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
        textMiner = Coin.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
        textCoin = Coin.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        textOre = Coin.transform.GetChild(9).GetComponent<TextMeshProUGUI>();
        ThePlayer = GameObject.Find("Player");
       

        ThePlayer = GameObject.Find("/Character/Player");

   

        playerStaminaManager = ThePlayer.GetComponent<PlayerStaminaManager>();
        playerHealthManager = ThePlayer.GetComponent<PlayerHealthManager>();
        xpManager = ThePlayer.GetComponent<XpManager>();
        CharacterStatsManager = ThePlayer.GetComponent<CharacterStatsManager>();
        characterMovement2D = ThePlayer.GetComponent<CharacterMovement2D>();
        if (CanResetLvl)
            Load();
    }
    private void Start()
    {
        if(Boat !=null)
        Boat.transform.position = SpawnPosition.position;
       
        //  PlayerPrefs.DeleteAll();
    }

    private void Update()
    {


        if (ThePlayer == null)
            RunAwake();

        StartCoroutine(AutoSave5s());
        textMinerMax.text = maxMining.ToString();
        textMiner.text = CurrentMining.ToString();
        textCoin.text = coin.ToString();
        textOre.text = IronOre.ToString();

        if(CurrentMining > maxMining)
        {
            CurrentMining = maxMining;
        }
        if(CurrentMining < 0)
        {
            CurrentMining = 0;
        }
     
        if(maxMining == 0)
        {
            maxMining = 10;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

            Load();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.DeleteAll();

        }
        
     
    }


    
    public void ReturnToBoat()
    {
        playerTransform.position = SpawnPosition.position;
        //RespawnUICanvas.SetActive(true);
        
    }
    IEnumerator RespawnUI()
    {
        yield return new WaitForSeconds(2f);

        
        
        
        
        yield return new WaitForSeconds(2);
        RespawnUICanvas.SetActive(false);
        StopCoroutine(RespawnUI());
    }

   
    private void Save()
    {
        if (ThePlayer == null)
            return;
        int CoinAmount = coin;

        if(playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;

            PlayerPrefs.SetFloat("playerpositionX", playerPosition.x);
            PlayerPrefs.SetFloat("playerpositionY", playerPosition.y);
        }
        


    
        PlayerPrefs.SetInt("MaxMiningAmount", maxMining);
        PlayerPrefs.SetInt("OreT1Amount", IronOre);

        PlayerPrefs.SetInt("levelAmount", levelAmount);
         PlayerPrefs.SetInt("TargetXPAmount", targetXPAmount);
         PlayerPrefs.SetInt("CurrentXPAmount", currentXPAmount);

        PlayerPrefs.SetFloat("StartingHealthAmount", playerHealthManager.startingHealth);
        PlayerPrefs.SetFloat("currentHealthAmount", playerHealthManager.currentHealth);

        PlayerPrefs.SetInt("StartingStamAmount", playerStaminaManager.startingStamina);
        PlayerPrefs.SetInt("currentStamAmount", playerStaminaManager.currentStamina);


        PlayerPrefs.SetFloat("CurrentJumpHeightAmount", characterMovement2D.maxJumpHeight);
        PlayerPrefs.SetFloat("CurrentSpeedAmount", characterMovement2D.MaxGroundSpeed);

        PlayerPrefs.SetInt("CurrentPoints", CharacterStatsManager.StatsPoint);

       

        PlayerPrefs.SetInt("coinAmount", coin);

        PlayerPrefs.Save();
      //  Debug.Log("saved");

    }
    public void Load()
    {
        
        if (PlayerPrefs.HasKey("playerpositionX"))
        {
                      float playerPositionX = PlayerPrefs.GetFloat("playerpositionX");
                      float playerPositionY = PlayerPrefs.GetFloat("playerpositionY");
              Vector3 playerPosition = new Vector3(playerPositionX, playerPositionY);
            //   CodeMonkey.CMDebug.TextPopupMouse("" + playerPosition);
         
            int MaxMiningAmount = PlayerPrefs.GetInt("MaxMiningAmount", 0);
            int OreT1Amount = PlayerPrefs.GetInt("OreT1Amount", 0);

            int CoinAmount = PlayerPrefs.GetInt("coinAmount",0);

            float JumpHeightAmount = PlayerPrefs.GetFloat("CurrentJumpHeightAmount", 0);
            float SpeedAmount = PlayerPrefs.GetFloat("CurrentSpeedAmount", 0);

            int LevelAmount = PlayerPrefs.GetInt("levelAmount", 0);
            int TargetXPAmount = PlayerPrefs.GetInt("TargetXPAmount", 0);
            int CurrentXPAmount = PlayerPrefs.GetInt("CurrentXPAmount", 0);

            float CurrentHealthAmount = PlayerPrefs.GetFloat("currentHealthAmount", 0);
            float StartingHealthAmount = PlayerPrefs.GetFloat("StartingHealthAmount", 0);

            int CurrentStamAmount = PlayerPrefs.GetInt("currentStamAmount", 0);
            int StartingStamAmount = PlayerPrefs.GetInt("StartingStamAmount",200);


            characterMovement2D.maxJumpHeight = JumpHeightAmount;
            characterMovement2D.MaxGroundSpeed = SpeedAmount;

            playerHealthManager.startingHealth = StartingHealthAmount;
            playerHealthManager.currentHealth = CurrentHealthAmount;

            playerStaminaManager.startingStamina = StartingStamAmount;
            playerStaminaManager.currentStamina = CurrentStamAmount;

            levelAmount = LevelAmount;
            targetXPAmount = TargetXPAmount;
            currentXPAmount = CurrentXPAmount;

            IronOre = OreT1Amount;
        
            maxMining = MaxMiningAmount;




            int StatsPointAmount = PlayerPrefs.GetInt("CurrentPoints", 0);

            playerTransform.transform.SetPositionAndRotation(playerPosition, Quaternion.Euler(new Vector3(0, 0, 0)));

            coin = CoinAmount;

            xpManager.LoadSetValues();
            CharacterStatsManager.StatsPoint = StatsPointAmount;

        }
        else
        {
         //   Debug.Log("no save");
            //no safe avalable
        }
    }
    
    
    IEnumerator AutoSave5s()
    {

           yield return new WaitForSeconds(1f);
           Save();

    }

    
}
