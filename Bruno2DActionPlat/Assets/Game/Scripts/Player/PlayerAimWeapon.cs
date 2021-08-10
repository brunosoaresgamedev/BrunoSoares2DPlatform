using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;
using System;


public class PlayerAimWeapon : MonoBehaviour
{
  public  bool currentlyshooting;
    public float waitBetweenShots;
    private float betweenShotCounter;
    public AudioSource gunSound;
    bool isShooting;
    public GameObject bullet;
    public GameObject bulletSpawner;
    public GameObject BulletEffectStandart;
    public Transform bulletPoint;
    bool CanSpendStamina;
    public int numberOfBullets;
    public bool CanShoot;
    public LayerMask notToHit;
    private Transform aimTransform;
    PlayerInput playerInput;
    public Animator aimAnimator;
    [SerializeField]
    int StaminaSpendRate;
    bool bulletspawnerisactive;
    bool bulletspawnerisnotactive;
    daggerSpawn daggerspawn;
    [SerializeField]
    float BulletRateTime;
    public int clickcounter = 0;
    Vector3 touchPosition;
    public int StartingStamina;
    public int currentStamina;
    private void Start()
    {
        
        CanShoot = true;
        currentStamina = StartingStamina;
        
        
    }
    private void Awake()
    {
        daggerspawn = GetComponent<daggerSpawn>();
        playerInput = GetComponent<PlayerInput>();
        aimAnimator = GetComponent<Animator>();
        aimTransform = transform.Find("Aim");
    }
    private void Update()
    {
  
        HandleShooting();
        Gettouch();
        HandleAiming();
    }
   
    void HandleShooting()
    {
        if (playerInput.IsRangeAttackButtonDown() && clickcounter == 0)
        {
            StartCoroutine(ShootingLop());
            StartCoroutine(quickDelay1());
        }
        if (playerInput.IsRangeAttackButtonDown() && clickcounter == 1)
        {
            StopAllCoroutines();
            BulletEffectStandart.SetActive(false);
            StartCoroutine(quickDelay0());
        }
 
    }
    IEnumerator quickDelay0()
    {
        yield return new WaitForSeconds(0.1f);
        clickcounter = 0;
    }
    IEnumerator quickDelay1()
    {
        yield return new WaitForSeconds(0.1f);
        clickcounter = 1;
    }
   void Gettouch()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        for (int i = 0; i < Input.touchCount; i++)
             {
              touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
             Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
     
             }

    }

    private void HandleAiming()
    {


//#if UNITY_EDITOR
        //      Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
//#endif

        Vector3 aimDirection = (touchPosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        

            
  
    }


    public void TakeStamina(int stamina)
    {
      
            currentStamina -= stamina;
 
    }

   public IEnumerator StaminaSpend()
    {
        CanSpendStamina = false;

        yield return new WaitForSeconds(1);
        
        CanSpendStamina = true;
    }

    IEnumerator ShootingLop()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletRateTime);
            Shoot();
        }
    }

    
    public void Shoot()
    {
        if (!isShooting && CanShoot)
        {
            StartCoroutine(BulletRate());
            StartCoroutine(BulletDaggerEffect());
        }

        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
    }


    IEnumerator BulletRate()
    {
        CanShoot = false;
        yield return new WaitForSeconds(BulletRateTime);
        CanShoot = true;
    }
    IEnumerator BulletDaggerEffect()
    {
        isShooting = true;
        BulletEffectStandart.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
        BulletEffectStandart.SetActive(false);
    }


}
