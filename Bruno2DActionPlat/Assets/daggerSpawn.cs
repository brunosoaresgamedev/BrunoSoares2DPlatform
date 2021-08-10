using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daggerSpawn : MonoBehaviour
{
    public bool currentlyshooting;

    [SerializeField]
    float BulletAmount;
    bool isShooting;
    public GameObject bullet;
    public GameObject BulletEffectStandart;
    public Transform bulletPoint;
    
  
   bool CanShoot;
    public bool DaggerSpawnActive;
   
    
   
    // Start is called before the first frame update
    void Start()
    {
      DaggerSpawnActive = true;
    CanShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (CanShoot)
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        if (!isShooting)
        {
            StartCoroutine(BulletRate());
            StartCoroutine(BulletDaggerEffect());
        }
        
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
    }


    IEnumerator BulletRate()
    {
        CanShoot = false;
        yield return new WaitForSeconds(BulletAmount);
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
