using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PlayerAimWeapon : MonoBehaviour
{
    bool isAlreadyAim;
    [SerializeField]
    GameObject ImageAttackRefe;
    [SerializeField]
    bool IsAiming;

    public bool currentlyshooting;
    public float waitBetweenShots;
    private float betweenShotCounter;
    public AudioSource gunSound;
    bool isShooting;
    bool isMoving;
    public GameObject bullet;
    public GameObject bulletSpawner;
    public GameObject BulletEffectStandart;
    public Transform bulletPoint;

    bool canGetTouch;



    [SerializeField]
    GameObject Vccam1, Vccam2;
    [SerializeField]
    GameObject background1, backgroundMundi;
    public int numberOfBullets;
    public bool CanShoot;
    public LayerMask notToHit;
    private Transform aimTransform;
    PlayerInput playerInput;
    public Animator aimAnimator;
    [SerializeField]

    bool bulletspawnerisactive;
    bool bulletspawnerisnotactive;
    daggerSpawn daggerspawn;
    [SerializeField]
    float BulletRateTime;
    [SerializeField]
    private int clickcounter = 0;
    Vector3 touchPosition;
    public bool canAim;
    [SerializeField]
    Text tex;
    public bool canrecoverstam;
    [SerializeField]
    PlayerStaminaManager playerStaminaManager;

    [SerializeField]
    GameObject buttonUIRangedAttack;

    [SerializeField]
    Animator animFX;

    private void Start()
    {
        canAim = true;
        CanShoot = true;
        canrecoverstam = true;
        IsAiming = false;

    }
    private void Awake()
    {
        canAim = true;
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        daggerspawn = GetComponent<daggerSpawn>();
        playerInput = GetComponent<PlayerInput>();
        aimAnimator = GetComponent<Animator>();
        aimTransform = transform.Find("Aim");
    }
    private void FixedUpdate()
    {



        if (playerInput.IsUsingTouchAny)
        {
            HandleShooting();
            GetClickCounter();
            //  ClickCounter();
            Gettouch();
        }


        // ImageAttackRefe.transform.position = touchPosition;

        if (playerStaminaManager.currentStamina > 0 && canAim)
        {
            if (!playerInput.IsUsingTouchAny)
                HandleShooting();
            HandleAiming();
        }
        else
        {
            Vccam1.SetActive(true);
            Vccam2.SetActive(false);
            BulletEffectStandart.SetActive(false);
            clickcounter = 0;
            canrecoverstam = true;
            playerStaminaManager.currentStamina += 1;
            StopAllCoroutines();
        }
    }
    private void Update()
    {

    }
    public void ClickCounter()
    {
        clickcounter++;
        StartCoroutine(quickDelay1());
    }
    void HandleShooting()
    {
        if (playerInput.IsUsingTouchAny)
        {
            if (clickcounter == 2 && IsAiming == false)
            {

                clickcounter = 0;
                IsAiming = true;
                Vccam1.SetActive(false);
                Vccam2.SetActive(true);
                canrecoverstam = false;
                StartCoroutine(ShootingLop());
                //   StopCoroutine(quickDelay1());


            }

            if (clickcounter == 2 && IsAiming == true)
            {
                clickcounter = 0;
                IsAiming = false;
                Vccam1.SetActive(true);
                Vccam2.SetActive(false);
                canrecoverstam = true;
                StopAllCoroutines();
                BulletEffectStandart.SetActive(false);
                //    StartCoroutine(quickDelay1());
            }
        }
        if (playerInput.IsUsingJoystickAim)
        {
            if (playerInput.IsAiming)
            {
                if (!isAlreadyAim)
                    StartAim();
            }
            if (!playerInput.IsAiming)
            {
                StopAim();
            }
        }
    }

    void StopAim()
    {
        isAlreadyAim = false;
        IsAiming = false;
        Vccam1.SetActive(true);
        Vccam2.SetActive(false);
        canrecoverstam = true;
        StopAllCoroutines();
        BulletEffectStandart.SetActive(false);
    }
    void StartAim()
    {
        animFX.speed = 1 / BulletRateTime;
        isAlreadyAim = true;
        IsAiming = true;
        Vccam1.SetActive(false);
        Vccam2.SetActive(true);
        canrecoverstam = false;
        StartCoroutine(ShootingLop());
    }
    IEnumerator quickDelay0()
    {
        yield return new WaitForSeconds(0.1f);
        clickcounter++;
    }
    IEnumerator quickDelay1()
    {
        yield return new WaitForSeconds(0.3f);
        clickcounter = 0;
    }
    IEnumerator quickDelay2()
    {
        yield return new WaitForSeconds(0.1f);
        clickcounter = 1;
    }

    void GetClickCounter()
    {

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())

        {
            StartCoroutine(quickDelay1());
            clickcounter++;
        }
    }


    void Gettouch()
    {
        //   if (canGetTouch)
        //   {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (i > 1)
                i = 0;


            if (i >= 1 && !IsPointerOverUIObject())
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i = 1].position);
            }
            touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            // Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        }

        //   }


    }



    public void UpdateIfCanAim(Vector2 movementInput)
    {

        if (movementInput.x != 0)
        {

            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void HandleAiming()
    {
        //#if UNITY_EDITOR
        //   Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        //#endif

        if (playerInput.IsUsingJoystickAim)
        {
            Vector2 aimDirection = playerInput.GetAttackInput();

            canGetTouch = false;
            //
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);


        }



        if (playerInput.IsUsingTouchAny)
        {
            if (!IsPointerOverUIObject())
            {
                Vector3 aimDirection = (touchPosition - transform.position).normalized;
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                aimTransform.eulerAngles = new Vector3(0, 0, angle);
            }

        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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

        }
        playerStaminaManager.LostStamPlayer(1);
        BulletEffectStandart.SetActive(true);
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
