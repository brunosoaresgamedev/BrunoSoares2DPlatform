using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinerScript : MonoBehaviour
{


    [SerializeField]
    public Text OreRateText;
    public bool InUse;
    bool startGettingOre;
    bool startRecoverRate;
    LvlMenuLeanTwen MYLeanTwen;
    public int GatherOreRate;

    public int MinerTimerMax = 60;
    public int MinerTimerCurrent;

    OreScript oreScript;

    public float GetOreRate;



    [SerializeField] private Image healthBarFillImage;
  
 

    private void Awake()
    {
        InUse = true;
        MYLeanTwen = GetComponent<LvlMenuLeanTwen>();
    }

    private void Update()
    {
        if (InUse)
        {

            GetOre();

            if (MinerTimerCurrent >= MinerTimerMax)
            {
                InUse = false;
                StartCoroutine(RecoverMiner());
                startRecoverRate = true;
                StartCoroutine(DestroyMiner());

            }
        }
        /*

          if (!InUse)
          {
              if (!startRecoverRate)
              {
                  if (MinerTimerCurrent >= MinerTimerMax && MinerTimerCurrent >= 0)
                  {
                      StartCoroutine(RecoverMiner());
                      startRecoverRate = true;

                  }


              }

            else if(MinerTimerCurrent <= 0)
              {
                  InUse = true;
                  startRecoverRate = false;
                  startGettingOre = false;

                   MYLeanTwen.Open();

              }

          }
        */


        GetOreRate = 60 / GatherOreRate;
        OreRateText.text = GetOreRate.ToString();

        

    }


    void GetOre()
    {
        if (!startGettingOre)
        {
            StartCoroutine(UpdateTimerOre());
            StartCoroutine(GettingOre());
            startGettingOre = true;
        }
    }

    IEnumerator DestroyMiner()

    {

        GameManager.instance.IsMinerDead--;
        GameManager.instance.CurrentMining--;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
    IEnumerator RecoverMiner()

    {
        while (MinerTimerCurrent > 0)
        {
            yield return new WaitForSeconds(1);

            MinerTimerCurrent--;
        }

    }
    IEnumerator GettingOre()
    {
        while (MinerTimerCurrent < MinerTimerMax)
        {
            yield return new WaitForSeconds(GatherOreRate);
            GameManager.instance.IronOre++;
        }



    }
    IEnumerator UpdateTimerOre()
    {
        while (MinerTimerCurrent < MinerTimerMax)
        {
            yield return new WaitForSeconds(1);

            MinerTimerCurrent++;
        }

    }



    private void LateUpdate()
    {
        float healthPercent = (float)MinerTimerCurrent / MinerTimerMax;
        healthBarFillImage.fillAmount = healthPercent;
    }



}
